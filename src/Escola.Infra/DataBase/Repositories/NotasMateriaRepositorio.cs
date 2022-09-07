using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Infra.DataBase.Repositories
{
    public class NotasMateriaRepositorio : INotasMateriaRepositorio
    {
        private readonly EscolaDBContexto _contexto;

        public NotasMateriaRepositorio(EscolaDBContexto contexto)
        {
            _contexto = contexto;
        }

        public void AtualizarNotas(NotasMateria notasMateria)
        {
            _contexto.NotasMaterias.Update(notasMateria);
            _contexto.SaveChanges();
        }

        public void ExcluirNotas(NotasMateria notasMateria)
        {
            _contexto.NotasMaterias.Remove(notasMateria);
            _contexto.SaveChanges();
        }

        public void InserirNotas(NotasMateria notasMateria)
        {
            _contexto.NotasMaterias.Add(notasMateria);
            _contexto.SaveChanges();
        }

        public NotasMateria ObterPorId(int id)
        {
            return _contexto.NotasMaterias.Find(id);
        }

        public List<NotasMateria> ObterTodos()
        {
            return _contexto.NotasMaterias.ToList();
        }
    }
}
