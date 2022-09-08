using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Api.Config;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Escola.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Escola.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaServico _materiaServico;
        private readonly CacheService<MateriaDTO> _cacheServicePorId;
        private readonly CacheService<IList<MateriaDTO>> _cacheServicePorNome;
        public MateriasController(IMateriaServico materiaServico, 
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
        public IActionResult ObterTodos ([FromQuery] string nome, 
                                        [FromQuery] int skip = 0, 
                                        [FromQuery] int take = 5){
            IList<MateriaDTO> materias;
            if(!string.IsNullOrEmpty(nome)){
                if (!_cacheServicePorNome.TryGetValue(nome,out  materias))
                {
                    materias = _materiaServico.ObterPorNome(nome);
                    _cacheServicePorNome.Set(nome,materias);
                }
            }
            else {
                var paginacao = new Paginacao(take,skip);
                materias = _materiaServico.ObterTodos(paginacao);
            }
            var uriBase = $"{Request.Scheme}://{Request.Host}";
            if (materias.Count >0 &&
                materias.First().Links == null){
                foreach (var materia in materias){
                    materia.Links = GerarHateoasMateria(uriBase,materia);
                }
            }
            var retorno = new BaseDTO<IList<MateriaDTO>>(){
                Data =materias,
                Links = GerarHateoasMateriasLista(uriBase,take,skip, 3)
                };

            return Ok(retorno);
        }
        [HttpGet("{materiaId}")]
        public IActionResult ObterPorId ( [FromRoute]int materiaId){
            if(!_cacheServicePorId.TryGetValue($"{materiaId}",out MateriaDTO materia))
            {
                materia = _materiaServico.ObterPorId(materiaId);

                var uriBase = $"{Request.Scheme}://{Request.Host}";

                materia.Links = GerarHateoasMateria(uriBase,materia);

                _cacheServicePorId.Set(materiaId.ToString(), 
                                        materia);
            }
            return Ok(materia);
        }
        private IList<HateoasDTO> GerarHateoasMateria(string url, MateriaDTO materia){
            return new List<HateoasDTO>(){
                new HateoasDTO(){
                    Rel = "self",
                    Type = "get",
                    URI = $"{url}/api/materias/{materia.Id}"
                },
                new HateoasDTO(){
                    Rel = "materia",
                    Type = "delete",
                    URI = $"{url}/api/materias/{materia.Id}"
                },
                new HateoasDTO(){
                    Rel = "materia",
                    Type = "put",
                    URI = $"{url}/api/materias/{materia.Id}"
                }
            };
        }

        private IList<HateoasDTO> GerarHateoasMateriasLista(string url,int take, int skip, int total){
            var lista =  new List<HateoasDTO>(){
                new HateoasDTO(){
                    Rel = "self",
                    Type = "get",
                    URI = $"{url}/api/materias?take={take}&skip={skip}"
                }
            };

            var razao =  take - skip;

            if(skip > 0 ){
                var newSkip = skip - razao;
                if (newSkip < 0)
                    newSkip = 0;
                
                lista.Add(new HateoasDTO(){
                    Rel = "prev",
                    Type = "get",
                    URI = $"{url}/api/materias?take={take-razao}&skip={newSkip}"
                });

            }

            if (take < total)
            { 
                lista.Add(new HateoasDTO(){
                    Rel = "next",
                    Type = "get",
                    URI = $"{url}/api/materias?take={take+razao}&skip={skip+razao}"
                });
            }

            return lista;
        }
    }
}