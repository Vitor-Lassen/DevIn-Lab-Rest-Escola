using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class AlunoRepositorio :  BaseRepositorio<Aluno,Guid>, IAlunoRepositorio
    {
        public AlunoRepositorio(EscolaDBContexto contexto) : base (contexto)
        {
        }
      
        public bool ExisteMatricula(int matricula)
        {
            return _contexto.Set<Aluno>().Any(x => x.Matricula == matricula);
        }

        public int ObterTotal()
        {
            return _contexto.Alunos.Count();
        }
        public override Aluno ObterPorId(Guid id){
            return _contexto.Set<Aluno>().Where(x => x.Id == id).First();
        }
        
    }
}