using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Escola.Domain.Interfaces.Services;
using Escola.Domain.DTO;

namespace Escola.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/materia")]
    public class MateriaV2Controller : ControllerBase
    {
        private readonly IMateriaServico _materiaServico;
        public MateriaV2Controller(IMateriaServico materiaServico)
        {
            _materiaServico = materiaServico;
        }
        [HttpGet]
        public IActionResult ObterTodos(){
            return Ok(_materiaServico.ObterTodos().Select(x => new MateriaV2DTO(x)));
        } 
        [HttpGet("{id}")]
        public IActionResult ObterPorId([FromRoute] int id){
            return Ok(new MateriaV2DTO(_materiaServico.ObterPorId(id)));
        }   

        [HttpPost]
        public IActionResult Adicionar ([FromBody] MateriaV2DTO materia){
            _materiaServico.Inserir(new MateriaDTO(materia));

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar ([FromRoute] int id, [FromBody] MateriaV2DTO materia){
        materia.Id = id;

        _materiaServico.Atualizar(new MateriaDTO(materia));
        return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover ([FromRoute] int id){

            _materiaServico.Excluir(id);
            return Ok();
        }
    }
}