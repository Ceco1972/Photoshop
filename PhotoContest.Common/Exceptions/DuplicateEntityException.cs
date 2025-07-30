using System;
namespace PhotoContest.Common.Exceptions
{
    public class DuplicateEntityException : ArgumentException
    {
        public DuplicateEntityException(string message) 
            : base(message)
        {

        }
    }
}
