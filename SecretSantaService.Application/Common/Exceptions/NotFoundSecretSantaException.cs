using System;

namespace SecretSantaService.Application.Common.Exceptions
{
    public class NotFoundSecretSantaException : SecretSantaException
    {
        public NotFoundSecretSantaException()
        {
        }

        public NotFoundSecretSantaException(string message) : base(message)
        {
        }

        public NotFoundSecretSantaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
