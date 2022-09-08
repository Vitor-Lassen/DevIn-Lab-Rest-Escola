using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.DTO
{
    public class AlunoDTO : BaseHateoasDTO
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
        public AlunoDTO(AlunoV2DTO aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            Email = aluno.Email;
            Matricula = aluno.RA;
            Sobrenome = aluno.Sobrenome;
            DataNascimento = aluno.DataNascimento;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Boletim { get {return $"http://localhost:5216/api/alunos/{Id}/Boletims" ; } private set {}}

        // private string _boletim;

        // public void SetBoletim( string value){
        //     if (Nome!= null)
        //     _boletim = value;
        // }
        // public string GetBoletim(){
        //     return _boletim;
        // }

        
    }
}