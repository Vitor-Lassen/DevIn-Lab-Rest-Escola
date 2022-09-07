using Escola.Domain.Interfaces.Repositories;
using Escola.Domain.Models;

namespace Escola.Infra.DataBase.Repositories
{
    public class AlunoRepositorio : BaseRepository<Aluno, Guid>, IAlunoRepositorio
    {
        public AlunoRepositorio(EscolaDBContexto contexto) : base(contexto)
        {}


        public override Aluno ObterPorId (Guid id){
            return _contexto.Alunos.Where(x => x.Id == id).FirstOrDefault();
        }
        public bool ExisteMatricula(int matricula)
        {
            return _contexto.Alunos.Any(x => x.Matricula == matricula);
        }
        public IList<Aluno> ObterTodos(Paginacao paginacao)
        {
            return _contexto.Alunos
                            .Take(paginacao.Take)
                            .Skip(paginacao.Skip)
                            .ToList();
        }
        public int ObterTotal()
        {
            return _contexto.Alunos.Count();
        }
    }
}