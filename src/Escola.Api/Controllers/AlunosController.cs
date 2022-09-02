using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Escola.Domain.Exceptions;
using Escola.Domain.Models;

namespace Escola.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoServico _alunoServico;
        public AlunosController(IAlunoServico alunoServico)
        {
            _alunoServico = alunoServico;
        }
        [HttpGet]
        public IActionResult ObterTodos(int skip, int take){
            try{
                var paginacao = new Paginacao(take,skip);

                var totalRegistros = _alunoServico.ObterTotal();

                Response.Headers.Add("X-Paginacao-TotalResgistros",totalRegistros.ToString() );

                return Ok(_alunoServico.ObterTodos(paginacao));
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id){
            try{
             return Ok(_alunoServico.ObterPorId(id));
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Inserir (AlunoDTO aluno){
            _alunoServico.Inserir(aluno);

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] AlunoDTO aluno){
            try{
                aluno.Id=id;
                _alunoServico.Atualizar(aluno);
                return Ok();
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id){
            try{
                _alunoServico.Excluir(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}