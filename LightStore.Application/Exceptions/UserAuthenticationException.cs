using System;

namespace LightStore.Application.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException(string login, string password)
            : base($"Incorrect password \"{password}\" for login \"{login}\".") { }
    }
}
