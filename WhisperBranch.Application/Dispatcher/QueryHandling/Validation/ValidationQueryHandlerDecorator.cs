using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhisperBranch.Application.Dispatcher.QueryHandling.Validation
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
        private readonly IValidator<TQuery> _validator;
        private readonly IQueryHandler<TQuery, TResult> _inner;

        public ValidationQueryHandlerDecorator(
            IValidator<TQuery> validator,
            IQueryHandler<TQuery, TResult> inner)
        {
            _validator = validator;
            _inner = inner;
        }

        public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(query, cancellationToken);
            return await _inner.Handle(query, cancellationToken);
        }
    }
}
