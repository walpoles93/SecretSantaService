using MediatR;
using SecretSantaService.Application.Common.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (System.Exception ex)
            {
                if (ex is ApplicationException) throw;
                else throw new UnknownSecretSantaException($"Unhandled exception thrown in {typeof(TRequest).Name}", ex);
            }
        }
    }
}
