
namespace Escola.Infra.DataBase.Repositories
{
    public abstract class BaseRepository <TEntity, Tkey> where TEntity : class
    {
        protected readonly EscolaDBContexto _contexto;
        public BaseRepository(EscolaDBContexto contexto)
        {
            _contexto = contexto;
        }

        public virtual void Inserir(TEntity entity)
        {
            _contexto.Set<TEntity>().Add(entity);
            _contexto.SaveChanges();
        }
        public virtual TEntity ObterPorId(Tkey id)
        {
            return _contexto.Set<TEntity>().Find(id);
        }
        public virtual void Atualizar(TEntity entity)
        {
            _contexto.Set<TEntity>().Update(entity);
            _contexto.SaveChanges();
        }

        public virtual void Excluir(TEntity entity)
        {
            _contexto.Set<TEntity>().Remove(entity);
            _contexto.SaveChanges();
        }

        public virtual IList<TEntity> ObterTodos()
        {
            return _contexto.Set<TEntity>().ToList();
        }
    }
}