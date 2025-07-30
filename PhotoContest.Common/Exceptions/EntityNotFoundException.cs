using System;
namespace PhotoContest.Common.Exceptions
{
    public class EntityNotFoundException : ArgumentException
    {
        public EntityNotFoundException(string message)
            : base(message) { }
    }
}
