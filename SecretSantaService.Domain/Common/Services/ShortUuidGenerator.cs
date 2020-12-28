using SecretSantaService.Domain.Common.Interfaces;
using System;

namespace SecretSantaService.Domain.Common.Services
{
    public class ShortUuidGenerator : IShortUuidGenerator
    {
        public string Next()
        {
            var guidString = Guid.NewGuid().ToString();

            return guidString.GetHashCode().ToString("x");
        }
    }
}
