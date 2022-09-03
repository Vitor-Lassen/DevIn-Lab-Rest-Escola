using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Escola.Api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaServico _materiaServico;
        public MateriaController(IMateriaServico materiaServico)
        {
            _materiaServico = materiaServico;
        }

        [HttpPost]
        public IActionResult Post ([FromBody] MateriaDTO materia){
            _materiaServico.Inserir(materia);
            return Ok();
        }
        
        [HttpDelete("{materiaId}")]
        public IActionResult Delete ( [FromRoute]int materiaId){
            _materiaServico.Excluir(materiaId);
            return Ok();
        }
        [HttpPut("{materiaId}")]
        public IActionResult Put ( [FromRoute]int materiaId,[FromBody] MateriaDTO materia){
            materia.Id = materiaId;
            _materiaServico.Atualizar(materia);
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
            
            return Ok(_materiaServico.ObterPorId(materiaId));
        }
        
    }
}