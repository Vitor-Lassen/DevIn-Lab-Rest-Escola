using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class BaseRepositorio<TEntity, Tkey> where TEntity : class
    {
        protected readonly EscolaDBContexto _contexto;
        public BaseRepositorio(EscolaDBContexto contexo)
        {
            _contexto = contexo;
        }

        public virtual void Inserir (TEntity entity){
            _contexto.Set<TEntity>().Add(entity);
            _contexto.SaveChanges();
        }
        public virtual void Atualizar (TEntity entity){
            _contexto.Set<TEntity>().Update(entity);
            _contexto.SaveChanges();
        }

        public virtual TEntity ObterPorId(Tkey id){
            return _contexto.Set<TEntity>().Find(id);
        }

        public virtual IList<TEntity> ObterTodos( Paginacao paginacao){
            return _contexto.Set<TEntity>()
                            .Take(paginacao.Take)
                            .Skip(paginacao.Skip)
                            .ToList();
        }
        
        public virtual void Excluir (TEntity entity){
            _contexto.Set<TEntity>().Remove(entity);
        }
    }
}