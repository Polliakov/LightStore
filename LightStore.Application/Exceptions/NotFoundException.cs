using System;

namespace LightStore.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key = null)
            : base($"Entity \"{name}\"{(key is null ? "" : $" { key}")} not found.") { }
    }
}
