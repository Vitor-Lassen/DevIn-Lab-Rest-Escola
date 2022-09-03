using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.Interfaces.Repositories
{
    public interface IMateriaRepositorio
    {
        IList<Materia> ObterTodos();
        Materia ObterPorId(int id);
        List<Materia> ObterPorNome(string nome);
        void Inserir(Materia materia);
        void Excluir (Materia materia);
        void Atualizar (Materia materia);
    }
}