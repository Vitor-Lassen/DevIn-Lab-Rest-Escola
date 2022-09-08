using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;
using Escola.Infra.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Infra.DataBase.Repositories
{
    public class BoletimRepositorio : BaseRepository<Boletim,int>, IBoletimRepositorio
    {
        public BoletimRepositorio(EscolaDBContexto contexto) : base(contexto)
        {        }
        public IList<Boletim> ObterPorIdAluno(Guid id)
        {
            return _contexto.Boletins.Where(b =>b.AlunoId == id).ToList();
        }
    }
}
