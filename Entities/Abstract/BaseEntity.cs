using Entities.Abstract;

namespace Entities.Concrete
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
