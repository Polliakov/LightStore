using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using System;

namespace LightStore.Persistence.Entities
{
    public class AppUser : ISoftDeletable
    {
        public Guid AppUserId { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public AppUserRole Role { get; set; }
        public DateTime? Deleted { get; set; }

        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
    }
}
