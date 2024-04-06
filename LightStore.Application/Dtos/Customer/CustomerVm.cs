using System;

namespace LightStore.Application.Dtos.Customer
{
    public class CustomerVm
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public Guid CartId { get; set; }
    }
}
