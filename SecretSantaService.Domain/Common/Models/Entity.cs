using SecretSantaService.Domain.Common.Interfaces;

namespace SecretSantaService.Domain.Common.Models
{
    public class Entity : IEntity
    {
        public int Id { get; private set; }
    }
}
