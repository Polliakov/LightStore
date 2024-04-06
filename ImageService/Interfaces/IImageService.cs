using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LightStore.ImageService.Interfaces
{
    public interface IImageService
    {
        string GetImageUri(Guid id);
        Task<string> SaveImage(IFormFile file, ImageType type, Guid id);
    }
}
