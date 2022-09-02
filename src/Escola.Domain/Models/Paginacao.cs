using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.Models
{
    public class Paginacao
    {
        public Paginacao(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }

        public int Take { get; set; }
        public int Skip { get; set; }
    }
}