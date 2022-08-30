using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.Interfaces.Repositories
{
    public interface IAlunoRepositorio
    {
        IList<Aluno> ObterTodos();
        Aluno ObterPorId(Guid id);
        void Inserir(Aluno aluno);
        void Excluir (Aluno aluno);

    }
}