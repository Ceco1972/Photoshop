using System;

namespace PhotoContest.Common.Exceptions
{
    public class AuthenticationException : ArgumentException
    {
    public AuthenticationException()
    { }
    public AuthenticationException(string message)
        : base(message)
    { }
}
}