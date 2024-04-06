using System;

namespace LightStore.ImageService.Exceptions
{
    public class ImageProcessingException : Exception
    {
        public ImageProcessingException(string message) : base(message) { }
    }
}
