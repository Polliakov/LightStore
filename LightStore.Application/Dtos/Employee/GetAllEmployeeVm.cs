using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.Employee
{
    public class GetAllEmployeeVm
    {
        public Guid AppUserId { get; set; }
        public string Email { get; set; }
        public AppUserRole Role { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
    }
}
