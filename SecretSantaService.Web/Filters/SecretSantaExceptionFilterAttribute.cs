﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecretSantaService.Application.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace SecretSantaService.Web.Filters
{
    public class SecretSantaExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public SecretSantaExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(NotFoundSecretSantaException), HandleNotFoundException },
                { typeof(UnknownSecretSantaException), HandleUnknownException },
                { typeof(ValidationSecretSantaException), HandleValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);

                return;
            }

            HandleUnknownException(context);

            base.OnException(context);
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundSecretSantaException;
            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationSecretSantaException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
