using System;

namespace LightStore.Persistence.Interfaces
{
    public interface ISoftDeletable
    {
        public DateTime? Deleted { get; set; }
    }
}
