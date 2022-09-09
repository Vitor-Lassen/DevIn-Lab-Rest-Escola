using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.DTO
{
    public class AlunoV2DTO
    {
        public AlunoV2DTO()
        {
            
        }
       

        public Guid Id { get; set; } = Guid.NewGuid();
        public int RA { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        
    }
}