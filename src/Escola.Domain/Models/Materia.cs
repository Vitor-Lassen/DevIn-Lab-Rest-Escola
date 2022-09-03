using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;

namespace Escola.Domain.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual List<NotasMateria> NotasMaterias { get; set; }

        public Materia()
        {
            
        }
        public Materia(MateriaDTO materia)
        {
            Id = materia.Id;
            Nome = materia.Nome;
        }

        public void Update (MateriaDTO materia){
            Nome = materia.Nome;
        }
    }
}