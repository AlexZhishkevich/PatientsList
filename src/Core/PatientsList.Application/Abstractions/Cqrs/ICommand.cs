using CSharpFunctionalExtensions;
using MediatR;

namespace PatientsList.Application.Abstractions.Cqrs
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
