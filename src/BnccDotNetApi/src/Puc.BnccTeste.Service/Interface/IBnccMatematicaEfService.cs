using Puc.BnccTeste.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Service.Interface
{
    public interface IBnccMatematicaEfService 
    {
        IEnumerable<BnccMatematicaEf> ListarTodos();
        IEnumerable<BnccMatematicaEf> ListarAnosMatematica(
            IEnumerable<string> materia, 
            bool todos,
            bool primeiroAno, 
            bool segundoAno, 
            bool terceiroAno,
            bool quartoAno, 
            bool quintoAno, 
            bool sextoAno, 
            bool setimoAno,
            bool oitavoAno,
            bool nonoAno
            );       
         BnccMatematicaEf ObterPeloId(int id);
        BnccMatematicaEf ObterPeloCodHab(string cod);
    }
}
