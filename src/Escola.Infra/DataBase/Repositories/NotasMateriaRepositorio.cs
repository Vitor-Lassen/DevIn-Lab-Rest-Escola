using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Infra.DataBase.Repositories
{
    public class NotasMateriaRepositorio : BaseRepository<NotasMateria,int> ,INotasMateriaRepositorio
    {

        public NotasMateriaRepositorio(EscolaDBContexto contexto) :base(contexto)
        {
        }
    }
}
