using System.Runtime.ConstrainedExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Escola.Api.Config
{
    //PADRÃO DE CACHE
    //métodos genéricos
    public class CacheService<TEntity> //TEntity -> tipo genérico de entidade
    {
        private readonly IMemoryCache _cache; //implementação da extensão
        private string _baseKey; //chave base -> aluno no caso
        private TimeSpan _expiracao; //importante para dar um tempo de expiração do cache
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void Config(string baseKey, TimeSpan expiracao)
        {
            _baseKey = baseKey;
            _expiracao = expiracao;
        }
        public TEntity Set(string parametro, TEntity entity)
        {
            return _cache.Set<TEntity>(MontarChave(parametro), entity, _expiracao);
        }
        public bool TryGetValue(string parametro, out TEntity entity)
        {
            return _cache.TryGetValue<TEntity>(MontarChave(parametro), out entity);
        }
        public void Remove(string parametro)
        {
            _cache.Remove(MontarChave(parametro));
        }

        private string MontarChave(string parametro) //monta o "corpo"
        {
            return $"{_baseKey}:{parametro}";
        }

        //out -> outra opção de retorno
    }
}