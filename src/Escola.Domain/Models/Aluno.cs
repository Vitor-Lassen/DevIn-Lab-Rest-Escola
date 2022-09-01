using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;

namespace Escola.Domain.Models
{
    public class Aluno
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public Aluno(AlunoDTO aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            Email = aluno.Email;
            Matricula = aluno.Matricula;
            Sobrenome = aluno.Sobrenome;
            DataNascimento = aluno.DataNascimento;
        }
        public Aluno()
        {
            
        }
        public void Update(Aluno aluno){
            Nome = aluno.Nome;
            Email = aluno.Email;
            Matricula = aluno.Matricula;
            Sobrenome = aluno.Sobrenome;
            DataNascimento = aluno.DataNascimento;
        }
         public void Update(AlunoDTO aluno){
            Nome = aluno.Nome;
            Email = aluno.Email;
            Matricula = aluno.Matricula;
            Sobrenome = aluno.Sobrenome;
            DataNascimento = aluno.DataNascimento;
        }
    }
}