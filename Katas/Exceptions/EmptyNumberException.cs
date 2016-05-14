using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Exceptions
{
    public class EmptyNumberException : Exception
    {
        public EmptyNumberException() : base("Empty value found within collection")
        {            
        }
    }
}
