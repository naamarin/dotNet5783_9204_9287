using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        string throwing;
        public InvalidInputException(string? message) : base(message) { throwing = message; }
        public override string ToString()
             => "The input " + throwing + " is invalid.\n";
    }
}
