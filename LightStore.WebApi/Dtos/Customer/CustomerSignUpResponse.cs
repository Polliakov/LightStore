using LightStore.Application.Dtos.Customer;

namespace LightStore.WebApi.Dtos.Customer
{
    public class CustomerSignUpResponse : CustomerAppUserVm
    {
        public string Token { get; set; }
    }
}
