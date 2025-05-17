using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhisperBranch.Domain.Interfaces;

namespace WhisperBranch.Application.Dispatcher.QueryHandling.Validation
{
    public class TransactionQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _inner;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> inner,
            IUnitOfWork unitOfWork)
        {
            _inner = inner;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var result = await _inner.Handle(query, cancellationToken);

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
