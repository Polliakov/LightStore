using LightStore.ImageService.ImageProfiles;
using LightStore.ImageService.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.ImageService
{
    internal class ImageService : IImageService
    {
        public ImageService(ImageServiceOptions options)
        {
           savePath = options.SavePath ?? 
                throw new ArgumentNullException(nameof(options), $"{nameof(ImageServiceOptions.SavePath)} is required.");
           url = options.Url ?? 
                throw new ArgumentNullException(nameof(options), $"{nameof(ImageServiceOptions.Url)} is required.");
        }

        private const string extension = ".webp";
        private const int guidDivider = 3;
        private readonly string url;
        private readonly string savePath;
        private readonly ImageProfileFactory imageProfileFactory = new();

        public string GetImageUri(Guid id)
        {
            return $"{url}/{GetUrn(id)}";
        }

        public async Task<string> SaveImage(IFormFile file, ImageType type, Guid id)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            var profile = imageProfileFactory.Create(type);

            ValidateExtension(file, profile);
            var img = await Image.LoadAsync(file.OpenReadStream());

            CropToProportion(img, profile);
            ResizeByMax(img, profile);

            var path = Path.Combine(savePath, GetUrn(id));
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            await img.SaveAsync(path, new WebpEncoder { Quality = profile.Quality });

            return path;
        }

        private static string GetUrn(Guid id)
        {
            string guid = id.ToString();
            return $"{guid[0..guidDivider]}/{guid[guidDivider..]}{extension}";
        }

        private static void ValidateExtension(IFormFile file, ImageProfile profile)
        {
            var fileExt = Path.GetExtension(file.FileName).ToLower();
            var isAllowed = profile.AllowedExtensions.Any(ext => ext == fileExt);
            if (!isAllowed)
                throw new ImageProcessingException($"Unsoported image format: \"{fileExt}\".");
        }

        private static void ResizeByMax(Image image, ImageProfile profile)
        {
            if (image.Width <= profile.MaxSize && image.Height <= profile.MaxSize)
                return;

            var options = new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(profile.MaxSize),
            };
            image.Mutate(action => action.Resize(options));
        }

        private static void CropToProportion(Image image, ImageProfile profile)
        {
            if (profile.Proportion <= 0) return;

            var expectedWidth = (int)(image.Height * profile.Proportion);
            if (expectedWidth < image.Width)
            {
                image.Mutate(action => action.Crop(expectedWidth, image.Height));
            }
            else if (expectedWidth > image.Width)
            {
                var expectedHeight = (int)(image.Width / profile.Proportion);
                image.Mutate(action => action.Crop(image.Width, expectedHeight));
            }
        }
    }
}
