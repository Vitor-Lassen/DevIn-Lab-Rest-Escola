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
using Microsoft.AspNetCore.JsonPatch;

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
                var uri = $"{Request.Scheme}://{Request.Host}";
                var alunos = new BaseDTO<IList<AlunoDTO>>(){
                    Data = _alunoServico.ObterTodos(paginacao),
                    Links = GetHateoasForAll(uri,take,skip, totalRegistros )};

                foreach (var aluno in alunos.Data){
                    aluno.Links = GetHateoas(aluno, uri);
                }

                return Ok(alunos);
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id){
            var cookie = Request.Cookies["TesteCookie"];
            var uri = $"{Request.Scheme}://{Request.Host}";
            AlunoDTO aluno;
            if (!_alunoCache.TryGetValue($"{id}", out aluno)){
                aluno = _alunoServico.ObterPorId(id);
                _alunoCache.Set(id.ToString(), aluno);
                aluno.Links = GetHateoas(aluno, uri);
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

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, 
                            [FromBody] JsonPatchDocument<AlunoDTO> alunoPatch)
        {
            var alunoDB = _alunoServico.ObterPorId(id);

            alunoPatch.ApplyTo(alunoDB, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            alunoDB.Id=id;
            _alunoServico.Atualizar(alunoDB);
           
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id){
                _alunoServico.Excluir(id);
                _alunoCache.Remove($"{id}");
                return StatusCode(StatusCodes.Status204NoContent);
        }
        private List<HateoasDTO> GetHateoas(AlunoDTO aluno, string baseURI){
            var hateoas =  new List<HateoasDTO>() {
                new HateoasDTO(){
                    Rel = "self",
                    Type = "GET", 
                    URI = $"{baseURI}/api/alunos/{aluno.Id}" 
                }, 
                new HateoasDTO(){
                    Rel = "aluno",
                    Type = "PUT", 
                    URI = $"{baseURI}/api/alunos/{aluno.Id}" 
                },
                new HateoasDTO(){
                    Rel = "aluno",
                    Type = "DELETE", 
                    URI = $"{baseURI}/api/alunos/{aluno.Id}" 
                }, 
                new HateoasDTO(){
                    Rel = "boletims",
                    Type = "GET", 
                    URI = $"{baseURI}/api/alunos/{aluno.Id}/boletims" 
                }
            };

            if((DateTime.Now.Year - aluno.DataNascimento.Year) >= 24 ){
                hateoas.Add(
                        new HateoasDTO(){
                        Rel = "MatricularAluno",
                        Type = "POST", 
                        URI = $"{baseURI}/api/alunos/{aluno.Id}/Matricular" 
                    }
                );
            }
            return hateoas;
        }
        private List<HateoasDTO> GetHateoasForAll( string baseURI, int take, int skip, int ultimo){
             var hateoas = new List<HateoasDTO>() {
                new HateoasDTO(){
                    Rel = "self",
                    Type = "GET", 
                    URI = $"{baseURI}/api/alunos?skip={skip}&take={take}" 
                }, 
          
                new HateoasDTO(){
                    Rel = "aluno",
                    Type = "POST", 
                    URI = $"{baseURI}/api/alunos/" 
                }
            };
            var razao = take - skip ;
            if (skip != 0){
                var newSkip =  skip - razao;
                if (newSkip < 0 )
                    newSkip = 0;
                
                hateoas.Add(new HateoasDTO(){
                    Rel = "Prev",
                    Type = "GET", 
                    URI = $"{baseURI}/api/alunos?skip={newSkip}&take={take-razao}" 
                });
            }

            if (take < ultimo){
                
                hateoas.Add(new HateoasDTO(){
                    Rel = "Next",
                    Type = "GET", 
                    URI = $"{baseURI}/api/alunos?skip={skip+razao}&take={take+razao}" 
                });    
            }


            return hateoas;
        }
    }
}