using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class MateriaRepositorio : IMateriaRepositorio
    {
        private readonly EscolaDBContexto _contexto;

        public MateriaRepositorio(EscolaDBContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Materia materia)
        {
            _contexto.Materias.Update(materia);
            _contexto.SaveChanges();
        }

        public void Excluir(Materia materia)
        {
            _contexto.Materias.Remove(materia);
            _contexto.SaveChanges();
        }

        public void Inserir(Materia materia)
        {
            _contexto.Materias.Add(materia);
            _contexto.SaveChanges();
        }

        public Materia ObterPorId(int id)
        {
            return _contexto.Materias.Find(id);
        }
        public List<Materia> ObterPorNome(string nome)
        {
            return _contexto.Materias.Where(x => x.Nome == nome).ToList();
        }

        public IList<Materia> ObterTodos()
        {
            return _contexto.Materias.ToList();
        }
    }
}