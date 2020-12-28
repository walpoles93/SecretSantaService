using System;

namespace SecretSantaService.Application.Common.Exceptions
{
    public class SecretSantaException : Exception
    {
        public SecretSantaException()
        {
        }

        public SecretSantaException(string message) : base(message)
        {
        }

        public SecretSantaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
