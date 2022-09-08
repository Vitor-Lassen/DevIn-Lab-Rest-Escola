using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Api.Config;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Escola.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaServico _materiaServico;
        private readonly CacheService<MateriaDTO> _cacheServicePorId;
        private readonly CacheService<IList<MateriaDTO>> _cacheServicePorNome;
        public MateriaController(IMateriaServico materiaServico, 
                                 CacheService<MateriaDTO> cacheServicePorId,
                                 CacheService<IList<MateriaDTO>> cacheServicePorNome)
        {
            _materiaServico = materiaServico;
            cacheServicePorId.Config("materiaPI", TimeSpan.FromHours(6));   
            cacheServicePorNome.Config("materiaPN", TimeSpan.FromHours(6)); 
            _cacheServicePorId = cacheServicePorId;
            _cacheServicePorNome = cacheServicePorNome;  
        }

        [HttpPost]
        public IActionResult Post ([FromBody] MateriaDTO materia){
            _materiaServico.Inserir(materia);
            return Ok();
        }
        
        [HttpDelete("{materiaId}")]
        public IActionResult Delete ( [FromRoute]int materiaId){
            _materiaServico.Excluir(materiaId);
            _cacheServicePorId.Remove($"{materiaId}");
            return Ok();
        }
        [HttpPut("{materiaId}")]
        public IActionResult Put ( [FromRoute]int materiaId,[FromBody] MateriaDTO materia){
            materia.Id = materiaId;
            _materiaServico.Atualizar(materia);
            _cacheServicePorId.Remove($"{materiaId}");

            _cacheServicePorNome.Remove(materia.Nome);
            
            return Ok();
        }
        //api/materia
        [HttpGet]
        public IActionResult ObterTodos ([FromQuery] string nome){
            if(!string.IsNullOrEmpty(nome)){
                if (!_cacheServicePorNome.TryGetValue(nome,out IList<MateriaDTO> materias))
                {
                    materias = _materiaServico.ObterPorNome(nome);
                    _cacheServicePorNome.Set(nome,materias);
                }
                return Ok(materias);
            }
            return Ok(_materiaServico.ObterTodos());
        }
        [HttpGet("{materiaId}")]
        public IActionResult ObterPorId ( [FromRoute]int materiaId){
            if(!_cacheServicePorId.TryGetValue($"{materiaId}",out MateriaDTO materia))
            {
                materia = _materiaServico.ObterPorId(materiaId);
                _cacheServicePorId.Set(materiaId.ToString(), 
                                        materia);
            }
            return Ok(materia);
        }
    }
}