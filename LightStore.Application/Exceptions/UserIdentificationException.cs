using System;

namespace LightStore.Application.Exceptions
{
    public class UserIdentificationException : Exception
    {
        public UserIdentificationException(string login)
            : base($"User with login \"{login}\" not found.") { }
    }
}
