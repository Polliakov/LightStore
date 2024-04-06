using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Application.Security;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class AppUserService : BaseDataService, IAppUserService
    {
        public AppUserService(IAppUserMapper appUserMapper, ILightStoreDbContext dbContext)
            : base(dbContext) 
        {
            this.appUserMapper = appUserMapper;
        }

        private readonly IAppUserMapper appUserMapper;

        public async Task<AppUserVm> Get(Guid id)
        {
            var appUser = await dbContext.AppUsers.AsNoTracking()
                .FirstOrDefaultAsync(au => au.AppUserId == id)
                ?? throw new NotFoundException(nameof(AppUser), id);
            return appUserMapper.MapToDto(appUser);
        }

        public async Task<AppUserVm> Authenticate(AppUserAuthDto authDto)
        {
            var appUser = await dbContext.AppUsers.FirstOrDefaultAsync(au => au.Email == authDto.Email);
            if (appUser is null)
                throw new UserIdentificationException(authDto.Email);
            if (!PasswordHash.CheckEqual(authDto.Password, appUser.Password))
                throw new UserAuthenticationException(authDto.Email, authDto.Password);

            return appUserMapper.MapToDto(appUser);
        }

        public async Task ChangePassword(Guid AppUserId, ChangePasswordDto dto)
        {
            var appUser = await dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.AppUserId == AppUserId);
            if (appUser is null)
                throw new NotFoundException(nameof(AppUser), AppUserId);

            if (!PasswordHash.CheckEqual(dto.OldPassword, appUser.Password))
                throw new UserAuthenticationException(appUser.Email, dto.OldPassword);

            appUser.Password = PasswordHash.Calculate(dto.NewPassword);
            await dbContext.SaveChangesAsync();
        }

        public async Task<AppUserVm> Create(CreateAppUserDto appUserDto)
        {
            var existing = await dbContext.AppUsers.AsNoTracking()
                .FirstOrDefaultAsync(au => au.Email == appUserDto.Email);
            if (existing is not null)
                throw new ExistsException(nameof(AppUser), existing.Email);

            var appUser = appUserMapper.MapFromDto(appUserDto);
            appUser.AppUserId = Guid.NewGuid();
            appUser.Password = PasswordHash.Calculate(appUserDto.Password);

            await dbContext.AppUsers.AddAsync(appUser);
            await dbContext.SaveChangesAsync();

            return appUserMapper.MapToDto(appUser);
        }

        public async Task Update(UpdateAppUserDto appUserDto)
        {
            var appUser = await dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.AppUserId == appUserDto.AppUserId)
                ?? throw new NotFoundException(nameof(AppUser), appUserDto.AppUserId);

            if (appUserDto.Password is not null)
                appUser.Password = PasswordHash.Calculate(appUserDto.Password);
            if (appUserDto.Email is not null)
                appUser.Email = appUserDto.Email;
            if (appUserDto.Role is not null)
                appUser.Role = appUserDto.Role.Value;

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            var appUser = await dbContext.AppUsers.FirstOrDefaultAsync(au => au.AppUserId == Id)
                ?? throw new NotFoundException(nameof(AppUser), Id);
            if (await IsLastAdmin(appUser))
                throw new NotAvailableException(nameof(AppUser), Id, "Deleting of the last admin.");

            appUser.Deleted = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsLastAdmin(AppUser user)
        {
            if (user.Role == AppUserRole.Admin)
            {
                var adminsCount = await dbContext.AppUsers
                    .Where(au => au.Role == AppUserRole.Admin).CountAsync();

                return adminsCount == 1;
            }
            return false;
        }
    }
}
