using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.DTO
{
    public abstract class BaseHateoasDTO
    {
        public IList<HateoasDTO> Links { get; set; }
    }
}