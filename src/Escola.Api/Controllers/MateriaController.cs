using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IMemoryCache _cache;
        public MateriaController(IMateriaServico materiaServico, IMemoryCache cache)
        {
            _materiaServico = materiaServico;
            _cache = cache;
        }

        [HttpPost]
        public IActionResult Post ([FromBody] MateriaDTO materia){
            _materiaServico.Inserir(materia);
            return Ok();
        }
        
        [HttpDelete("{materiaId}")]
        public IActionResult Delete ( [FromRoute]int materiaId){
            _materiaServico.Excluir(materiaId);
            _cache.Remove($"materia:{materiaId}");
            return Ok();
        }
        [HttpPut("{materiaId}")]
        public IActionResult Put ( [FromRoute]int materiaId,[FromBody] MateriaDTO materia){
            materia.Id = materiaId;
            _materiaServico.Atualizar(materia);
            _cache.Remove($"materia:{materiaId}");

            
            return Ok();
        }
        //api/materia
        [HttpGet]
        public IActionResult ObterTodos ([FromQuery] string nome){
            if(!string.IsNullOrEmpty(nome))
                return Ok(_materiaServico.ObterPorNome(nome));
            return Ok(_materiaServico.ObterTodos());
        }
        [HttpGet("{materiaId}")]
        public IActionResult ObterPorId ( [FromRoute]int materiaId){
            if(!_cache.TryGetValue<MateriaDTO>($"materia:{materiaId}",out MateriaDTO materia))
            {
                materia = _materiaServico.ObterPorId(materiaId);
                _cache.Set<MateriaDTO>($"materia:{materiaId}", 
                                        materia,
                                        TimeSpan.FromHours(5));
            }
            return Ok(materia);
        }
        
    }
}