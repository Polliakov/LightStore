using System;

namespace LightStore.Application.Exceptions
{
    public class ExistsException : Exception
    {
        public ExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) allready exists.") { }
    }
}
