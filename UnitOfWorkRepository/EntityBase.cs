using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkRepository
{
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}