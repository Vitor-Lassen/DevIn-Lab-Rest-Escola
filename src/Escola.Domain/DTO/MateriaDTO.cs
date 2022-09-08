using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Domain.DTO
{
    public class MateriaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
         public MateriaDTO()
        {
            
        }
        public MateriaDTO(Materia materia)
        {
            Id = materia.Id;
            Nome = materia.Nome;
        }
         public MateriaDTO(MateriaV2DTO materia)
        {
            Id = materia.Id;
            Nome = materia.Disciplina;

        }
    }
}