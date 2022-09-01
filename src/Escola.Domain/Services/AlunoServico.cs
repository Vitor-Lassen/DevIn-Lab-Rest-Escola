using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;
using Escola.Domain.Models;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Interfaces.Services;
using Escola.Domain.Exceptions;

namespace Escola.Domain.Services
{
    public class AlunoServico : IAlunoServico
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        public AlunoServico(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void Atualizar(AlunoDTO aluno)
        {
            var alunoDb = _alunoRepositorio.ObterPorId(aluno.Id);
            alunoDb.Update(aluno);
            //alunoDb.Update(new Aluno(aluno));
            _alunoRepositorio.Atualizar(alunoDb);

        }

        public void Excluir(Guid id)
        {
            var aluno = _alunoRepositorio.ObterPorId(id);
            _alunoRepositorio.Excluir(aluno);
        }

        public void Inserir(AlunoDTO aluno)
        {
            //ToDo: Validar se já consta matricula.
            if(_alunoRepositorio.ExisteMatricula(aluno.Matricula))
                throw new DuplicadoException("Matricula já existente");

            _alunoRepositorio.Inserir(new Aluno(aluno));
        }

        public AlunoDTO ObterPorId(Guid id)
        {
           return new AlunoDTO(_alunoRepositorio.ObterPorId(id));
        }

        public IList<AlunoDTO> ObterTodos()
        {
            // var alunosResposta = new List<AlunoDTO>();
            // var alunos=  _alunoRepositorio.ObterTodos();
            // foreach (var aluno in alunos){
            //     alunosResposta.Add(new AlunoDTO(aluno));
            // }
            // return alunosResposta;

            return _alunoRepositorio.ObterTodos()
                            .Select(x => new AlunoDTO(x)).ToList();
        }
    }
}