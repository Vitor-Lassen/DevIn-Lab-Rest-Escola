using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using Escola.Domain.Exceptions;
using Escola.Domain.Models;

namespace Escola.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoServico _alunoServico;
        private readonly IMemoryCache _cache;
        public AlunosController(IAlunoServico alunoServico, IMemoryCache cache)
        {
            _alunoServico = alunoServico;
            _cache = cache;
        }
        [HttpGet]
        public IActionResult ObterTodos(int skip = 0, int take = 5){
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
            AlunoDTO aluno;
            if (!_cache.TryGetValue<AlunoDTO>($"aluno:{id}", out aluno)){
                aluno = _alunoServico.ObterPorId(id);
                _cache.Set($"aluno:{id}", aluno, new TimeSpan(0,2,0));
            }
            return Ok(aluno);
        }
        [HttpPost]
        public IActionResult Inserir (AlunoDTO aluno){
            _alunoServico.Inserir(aluno);

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromBody] AlunoDTO aluno){
            
            aluno.Id=id;
            _alunoServico.Atualizar(aluno);
            _cache.Set($"aluno:{id}", aluno, new TimeSpan(0,2,0));
            return Ok();
           
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id){
            
                _alunoServico.Excluir(id);
                _cache.Remove($"aluno:{id}");
                return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}