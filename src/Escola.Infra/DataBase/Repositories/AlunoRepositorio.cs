using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly EscolaDBContexto _contexto;
        public AlunoRepositorio(EscolaDBContexto contexto)
        {
            _contexto = contexto;
        }
        public void Excluir(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Aluno aluno)
        {
            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();
        }

        public Aluno ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Aluno> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}