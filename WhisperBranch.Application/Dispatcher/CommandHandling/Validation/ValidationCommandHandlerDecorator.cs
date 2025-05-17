using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhisperBranch.Application.Dispatcher.CommandHandling.Validation
{
    public class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
      where TCommand : ICommand<TResult>
    {
        private readonly IValidator<TCommand> _validator;
        private readonly ICommandHandler<TCommand, TResult> _inner;

        public ValidationCommandHandlerDecorator(
            IValidator<TCommand> validator,
            ICommandHandler<TCommand, TResult> inner)
        {
            _validator = validator;
            _inner = inner;
        }

        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);
            return await _inner.Handle(command, cancellationToken);
        }
    }
}
