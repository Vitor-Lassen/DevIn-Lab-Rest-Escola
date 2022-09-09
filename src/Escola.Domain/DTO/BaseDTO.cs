using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Domain.DTO
{
    public class BaseDTO<TEntity> where TEntity : class //classe recebe um tipo genérico que seja uma classe
    {
        //Envelopamento do retorno, para aplicabilidade o ideal é que se a api retornar o dado envelopado deve-se usar como padrão para TODAS as rotas
        public TEntity Data { get; set; }
        public IList<HateoasDTO> Links { get; set; }

    }
}