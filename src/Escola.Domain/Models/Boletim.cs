using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.Models
{
    public class Boletim
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public DateTime order_date { get; set; }
    }
}