using Microsoft.EntityFrameworkCore;
using Puc.BnccTeste.Domain.Entidade;
using Puc.BnccTeste.Infra.Data.Context;
using Puc.BnccTeste.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Infra.Data.Repositorio
{
    public class BnccMatematicaEfRepositorio : RepositorioBase<BnccMatematicaEf>, IBnccMatematicaEfRepositorio
    {
        protected Contexto _Db;
        protected DbSet<BnccMatematicaEf> _DbSet;

        public BnccMatematicaEfRepositorio(Contexto contexto)
            : base(contexto) 
        {
            _Db = contexto;
            _DbSet = _Db.Set<BnccMatematicaEf>();
        }
                
        public IEnumerable<BnccMatematicaEf> ListarAnosMatematica(IEnumerable<string> materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            var lista = _Db.BnccMatematicaEfs.ToList();
            try
            {
                return lista;
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista = null;

        }       

        public IEnumerable<BnccMatematicaEf> ListarTodos()
        {
            var listar = _DbSet.ToList();
            try
            {
                if(listar != null)
                {
                    return listar;  
                }
               
            }
            catch (Exception ex)
            {
                listar = null;
            }
            return listar;
        }
    }
}
