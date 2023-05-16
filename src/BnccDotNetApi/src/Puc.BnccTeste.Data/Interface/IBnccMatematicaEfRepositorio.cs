using Puc.BnccTeste.Domain.Entidade;

namespace Puc.BnccTeste.Infra.Data.Interface
{
    public interface IBnccMatematicaEfRepositorio : IRepositorio<BnccMatematicaEf>
    {
        IEnumerable<BnccMatematicaEf> ListarAnosMatematica(IEnumerable<string> materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno);        
    }
}
