using System;

namespace LightStore.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string collectionName)
            : base($"Has duplicate in \"{collectionName}\".")
        { }
    }
}
