using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.Exceptions
{
    public class EhMenorIdadeException : Exception
    {
        public EhMenorIdadeException(string message) : base (message)
        {
            
        }
    }
}