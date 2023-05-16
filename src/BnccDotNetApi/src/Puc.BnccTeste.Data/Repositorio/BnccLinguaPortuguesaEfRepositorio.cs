using Microsoft.EntityFrameworkCore;
using Puc.BnccTeste.Domain.Entidade;
using Puc.BnccTeste.Infra.Data.Context;
using Puc.BnccTeste.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Infra.Data.Repositorio
{
    public class BnccLinguaPortuguesaEfRepositorio : RepositorioBase<BnccLinguaPortuguesaEf>, IBnccLinguaPortuguesaEfRepositorio
    {
        protected Contexto _Db;
        protected DbSet<BnccLinguaPortuguesaEf> _DbSet;

        public BnccLinguaPortuguesaEfRepositorio(Contexto contexto)
                : base(contexto)
        {
            _Db = contexto;
            _DbSet = _Db.Set<BnccLinguaPortuguesaEf>();
        }

        public IEnumerable<BnccLinguaPortuguesaEf> ListarAnosPortugues(bool materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            return _Db.BnccLinguaPortuguesaEfs.ToList();
        }
    }
}
