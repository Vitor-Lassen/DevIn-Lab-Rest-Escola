using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.DTO
{
    public class MateriaV2DTO
    {
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public MateriaV2DTO()
        {
            
        }
        public MateriaV2DTO(MateriaDTO materia)
        {
            Id = materia.Id;
            Disciplina = materia.Nome;

        }
    }
}