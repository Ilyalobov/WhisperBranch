using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhisperBranch.Domain.Interfaces;

namespace WhisperBranch.Application.Dispatcher.CommandHandling.Validation
{
    public class TransactionCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
           where TCommand : ICommand<TResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<TCommand, TResult> _inner;

        public TransactionCommandHandlerDecorator(
            IUnitOfWork unitOfWork,
            ICommandHandler<TCommand, TResult> inner)
        {
            _unitOfWork = unitOfWork;
            _inner = inner;
        }

        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await _inner.Handle(command, cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
