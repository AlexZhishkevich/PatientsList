using CSharpFunctionalExtensions;
using MediatR;

namespace PatientsList.Application.Abstractions.Cqrs
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
