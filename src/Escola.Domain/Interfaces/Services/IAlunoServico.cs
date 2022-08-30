using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.DTO;

namespace Escola.Domain.Interfaces.Services
{
    public interface IAlunoServico
    {
        IList<AlunoDTO> ObterTodos();
        AlunoDTO ObterPorId(Guid id);
        void Inserir(AlunoDTO aluno);
        void Excluir (AlunoDTO aluno);
    }
}