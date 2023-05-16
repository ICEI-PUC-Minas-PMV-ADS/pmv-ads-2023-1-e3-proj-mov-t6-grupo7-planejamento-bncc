using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Infra.Data.Interface
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> ListarTodos();               
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        TEntity ObterPeloId(int id);
        TEntity ObterPeloCodHab(string cod);
        public TEntity ObterPeloEmail(string email);
        int Salvar();
        bool Inserir(TEntity entidade);
        bool Atualizar(TEntity entidade);
        bool Deletar(int id);

        //obter competencias por materias
    }
}
