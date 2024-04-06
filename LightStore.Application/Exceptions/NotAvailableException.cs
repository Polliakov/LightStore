using System;

namespace LightStore.Application.Exceptions
{
    public class NotAvailableException : Exception
    {
        public NotAvailableException(string name, object key, string action)
            : base($"Entity \"{name}\" ({key}) not available for {action}.") { }
    }
}
