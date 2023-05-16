using Puc.BnccTeste.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Infra.Data.Interface
{
    public interface IBnccLinguaPortuguesaEfRepositorio : IRepositorio<BnccLinguaPortuguesaEf>
    {
        IEnumerable<BnccLinguaPortuguesaEf> ListarAnosPortugues(bool materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno);
    }
}
