using LightStore.Application.Dtos.Customer;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class CustomerService : BaseDataService, ICustomerService
    {
        public CustomerService(ILightStoreDbContext dbContext,
                               IAppUserService appUserService,
                               ICartService cartService,
                               ICustomerMapper customerMapper)
            : base(dbContext)
        {
            this.appUserService = appUserService;
            this.cartService = cartService;
            this.customerMapper = customerMapper;
        }

        private readonly IAppUserService appUserService;
        private readonly ICartService cartService;
        private readonly ICustomerMapper customerMapper;

        public async Task<CustomerVm> Get(Guid appUserId)
        {
            var customer = await dbContext.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.AppUserId == appUserId)
                ?? throw new NotFoundException(nameof(Customer), appUserId);
            return customerMapper.MapToDto(customer);
        }

        public async Task<CustomerAppUserVm> Create(CreateCustomerDto customerDto)
        {
            var cartId = customerDto.CartId;
            if (cartId is not null)
                await ValidateCart(cartId.Value);

            var customer = customerMapper.MapFromDto(customerDto);

            var createAppUserDto = customerMapper.Map(customerDto);
            createAppUserDto.Role = AppUserRole.Customer;

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var appUserVm = await appUserService.Create(createAppUserDto);
                customer.AppUserId = appUserVm.AppUserId;
                customer.CartId = cartId ?? await cartService.CreateCart();

                await dbContext.Customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return customerMapper.MapToDto((customer, appUserVm));
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task ValidateCart(Guid cartId)
        {
            if (!await cartService.IsCartExists(cartId))
                throw new NotFoundException(nameof(Cart), cartId);
            var cartOwnersId = await cartService.GetCartOwnersId(cartId);
            if (cartOwnersId is not null)
                throw new NotAvailableException(nameof(Cart), cartId, "attaching to customer");
        }

        public async Task<UpdateCustomerVm> Update(Guid AppUserId, UpdateCustomerDto dto)
        {
            var customer = await dbContext.Customers
                .FirstOrDefaultAsync(c => c.AppUserId == AppUserId)
                ?? throw new NotFoundException(nameof(Customer), AppUserId);

            customer.Surname = dto.Surname;
            customer.Name = dto.Name;
            customer.Patronymic = dto.Patronymic;

            await dbContext.SaveChangesAsync();
            return customerMapper.MapToUpdateDto(customer);
        }
    }
}
