using System.Collections.Generic;

namespace LightStore.ImageService.ImageProfiles
{
    internal abstract class ImageProfile
    {
        public ImageProfile()
        {
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        }

        public abstract int MaxSize { get; }
        public abstract double Proportion { get; }
        public virtual int Quality => 60;
        public virtual IReadOnlyList<string> AllowedExtensions { get; }
    }
}
