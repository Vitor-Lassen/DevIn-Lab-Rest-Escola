using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;
using Escola.Domain.Interfaces.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;
using Escola.Domain.Exceptions;

namespace Escola.Domain.Services
{
    public class MateriaServico : IMateriaServico
    {
        private readonly IMateriaRepositorio _materiaRepositorio;
        public MateriaServico(IMateriaRepositorio materiaRepositorio)
        {
            _materiaRepositorio = materiaRepositorio;
        }
        public void Atualizar(MateriaDTO materia)
        {
            var materiaDB = _materiaRepositorio.ObterPorId(materia.Id);
            //materiaDB.Nome = materia.Nome;

            materiaDB.Update(materia);
            _materiaRepositorio.Atualizar(materiaDB);
            
        }

        public void Excluir(int id)
        {
            var materia = _materiaRepositorio.ObterPorId(id);
            _materiaRepositorio.Excluir(materia);
        }

        public void Inserir(MateriaDTO materia)
        {
            var jaExiste = _materiaRepositorio.ObterPorNome(materia.Nome);

            if (jaExiste.Count > 0)
                throw new DuplicadoException("Matéria já existe");

            _materiaRepositorio.Inserir(new Materia(materia));
        }

        public MateriaDTO ObterPorId(int id)
        {
           return new MateriaDTO(_materiaRepositorio.ObterPorId(id));
        }

        public List<MateriaDTO> ObterPorNome(string nome)
        {
            return _materiaRepositorio.ObterPorNome(nome).Select(x => new MateriaDTO(x)).ToList();
        }

        public IList<MateriaDTO> ObterTodos()
        {
           return _materiaRepositorio.ObterTodos()
                                    .Select(x => new MateriaDTO(x)).ToList();
        }
    }
}