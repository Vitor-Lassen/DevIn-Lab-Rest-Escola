using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.DTO
{
    public class AlunoDTO
    {
        public AlunoDTO()
        {
            
        }
        public AlunoDTO(Aluno aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            Email = aluno.Email;
            Matricula = aluno.Matricula;
            Sobrenome = aluno.Sobrenome;
            DataNascimento = aluno.DataNascimento;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        
    }
}