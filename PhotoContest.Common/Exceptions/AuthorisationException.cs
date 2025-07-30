using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Common.Exceptions
{
    public class AuthorisationException : ArgumentException
    {
        public AuthorisationException(string message)
            : base(message)
        {}
    }
}
