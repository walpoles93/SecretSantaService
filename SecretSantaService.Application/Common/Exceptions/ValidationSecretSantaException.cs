using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace SecretSantaService.Application.Common.Exceptions
{
    public class ValidationSecretSantaException : SecretSantaException
    {
        public ValidationSecretSantaException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationSecretSantaException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
