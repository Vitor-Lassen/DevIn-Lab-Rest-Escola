using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.Exceptions
{
    public class DuplicadoException : Exception
    {
        public DuplicadoException(string nome) : base(nome)
        {
            
        }
    }
}