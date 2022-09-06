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
using Escola.Api.Config;

namespace Escola.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoServico _alunoServico;
        private readonly CacheService<AlunoDTO> _alunoCache;
        public AlunosController(IAlunoServico alunoServico, CacheService<AlunoDTO> alunoCache)
        {
            alunoCache.Config("aluno",new TimeSpan(0,2,0));
            _alunoCache = alunoCache;
            _alunoServico = alunoServico;
           
        }
        [HttpGet]
        public IActionResult ObterTodos(int skip = 0, int take = 5){
            try{
                var paginacao = new Paginacao(take,skip);

                var totalRegistros = _alunoServico.ObterTotal();

                Response.Headers.Add("X-Paginacao-TotalResgistros",totalRegistros.ToString() );

                Response.Cookies.Append("TesteCookie", 
                            Newtonsoft.Json.JsonConvert.SerializeObject(paginacao),
                            new CookieOptions(){
                                Expires = DateTimeOffset.Now.AddDays(5),
                                //MaxAge = new TimeSpan(5,0,0,0)
                            });

                return Ok(_alunoServico.ObterTodos(paginacao));
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id){
            var cookie = Request.Cookies["TesteCookie"];
            AlunoDTO aluno;
            if (!_alunoCache.TryGetValue($"{id}", out aluno)){
                aluno = _alunoServico.ObterPorId(id);
                _alunoCache.Set(id.ToString(), aluno);
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
            _alunoCache.Set($"{id}", aluno);
            return Ok();
           
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id){
            
                _alunoServico.Excluir(id);
                _alunoCache.Remove($"{id}");
                return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}