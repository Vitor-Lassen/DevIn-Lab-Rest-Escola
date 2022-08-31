using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;

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
        [HttpPost]
        public IActionResult Inserir (AlunoDTO aluno){
            try{
                _alunoServico.Inserir(aluno);
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}