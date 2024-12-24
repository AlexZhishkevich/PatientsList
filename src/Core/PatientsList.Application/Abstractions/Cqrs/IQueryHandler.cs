using CSharpFunctionalExtensions;
using MediatR;

namespace PatientsList.Application.Abstractions.Cqrs
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
