using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Common.Exceptions
{
    public class UnauthorisedOperationsException : ArgumentException
    {
        public UnauthorisedOperationsException(string message)
            : base(message)
        { }
    }
}
