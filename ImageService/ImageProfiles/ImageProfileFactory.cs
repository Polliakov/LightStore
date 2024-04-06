using System;

namespace LightStore.ImageService.ImageProfiles
{
    internal class ImageProfileFactory
    {
        public ImageProfile Create(ImageType imageType)
        {
            return imageType switch
            {
                ImageType.Product => new ProductImageProfile(),
                ImageType.Category => new CategoryImageProfile(),
                ImageType.Warehouse => new WarehouseImageProfile(),
                _ => throw new ArgumentException("Unknovn image type.")
            };
        }
    }
}
