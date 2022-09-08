using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class MateriaRepositorio : BaseRepository<Materia,int>, IMateriaRepositorio 
    {

        public MateriaRepositorio(EscolaDBContexto contexto) : base (contexto)
        {}

        public List<Materia> ObterPorNome(string nome)
        {
            return _contexto.Materias.Where(x => x.Nome == nome).ToList();
        }
        public IList<Materia> ObterTodos( Paginacao paginacao)
        {
            return _contexto.Materias.Take(paginacao.Take)
                                    .Skip(paginacao.Skip)
                                    .ToList();
        }
    }
}