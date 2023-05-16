using Microsoft.EntityFrameworkCore;
using Puc.BnccTeste.Infra.Data.Context;
using Puc.BnccTeste.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
 
namespace Puc.BnccTeste.Infra.Data.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected Contexto _Db;
        protected DbSet<TEntity> _DbSet;

        public RepositorioBase(Contexto contexto)
        {
            _Db = contexto;
            _DbSet= _Db.Set<TEntity>();
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return _DbSet.Where(predicate);
        }

        public IEnumerable<TEntity> ListarTodos()
        {
           return _DbSet.ToList();
        }

        public TEntity ObterPeloCodHab(string cod)
        {
            return _DbSet.Find(cod);
        }

        public TEntity ObterPeloId(int id)
        {
            return _DbSet.Find(id);
        }

        public TEntity ObterPeloEmail(string email)
        {
            var userEmail = email.ToString();
            return _DbSet.Find(email);
        }

        public int Salvar()
        {
            return _Db.SaveChanges();
        }

        public bool Inserir(TEntity entidade)
        {
           _DbSet.Add(entidade);
            return Salvar() > 0;
        }

        public bool Atualizar(TEntity entidade)
        {
            _DbSet.Update(entidade);
            return Salvar() > 0;
        }

        public bool Deletar(int id)
        {
            _DbSet.Remove(_DbSet.Find(id));
            return Salvar() > 0;
        }

        

        public void Dispose()
        {
            _Db.Dispose();
        }
    }
}
