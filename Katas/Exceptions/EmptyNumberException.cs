using System;

namespace Katas.Exceptions
{
    public class EmptyNumberException : ArgumentException
    {
        public EmptyNumberException() : base("Empty value found within collection")
        {            
        }
    }
}
