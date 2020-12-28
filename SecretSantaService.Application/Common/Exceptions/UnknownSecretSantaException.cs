using System;

namespace SecretSantaService.Application.Common.Exceptions
{
    public class UnknownSecretSantaException : SecretSantaException
    {
        public UnknownSecretSantaException()
        {
        }

        public UnknownSecretSantaException(string message) : base(message)
        {
        }

        public UnknownSecretSantaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
