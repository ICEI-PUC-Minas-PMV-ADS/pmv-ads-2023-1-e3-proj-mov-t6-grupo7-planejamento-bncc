using Puc.BnccTeste.Domain.Entidade;
using Puc.BnccTeste.Infra.Data.Interface;
using Puc.BnccTeste.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Service.Service
{
    public class BnccLinguaPortuguesaEfService : IBnccLinguaPortuguesaEfService
    {
        private readonly IBnccLinguaPortuguesaEfRepositorio _portRepo;

        public BnccLinguaPortuguesaEfService(IBnccLinguaPortuguesaEfRepositorio portRepo)
        {
            _portRepo= portRepo;
        }

        public IEnumerable<BnccLinguaPortuguesaEf> ListarAnosPortugues(IEnumerable<string> materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            var lista = _portRepo.ListarTodos();
            try
            {
                if(lista != null)
                {
                    if (todos)
                        return lista.ToList();

                    if (primeiroAno || segundoAno || terceiroAno || quartoAno || quintoAno || sextoAno || setimoAno || oitavoAno || nonoAno)
                    {
                        return lista.Where(
                            x => x.PrimeiroEf == primeiroAno && x.PrimeiroEf != false ||
                            x.SegundoEf == segundoAno && x.SegundoEf != false ||
                            x.TerceiroEf == terceiroAno && x.TerceiroEf != false ||
                            x.QuartoEf == quartoAno && x.QuartoEf != false ||
                            x.QuintoEf == quintoAno && x.QuintoEf != false ||
                            x.SextoEf == sextoAno && x.SextoEf != false ||
                            x.SetimoEf == setimoAno && x.SetimoEf != false ||
                            x.OitavoEf == oitavoAno && x.OitavoEf != false ||
                            x.NonoEf == nonoAno && x.NonoEf != false)
                            .ToList();
                    }

                    return lista.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public IEnumerable<BnccLinguaPortuguesaEf> ListarTodos()
        {
            var listar = _portRepo.ListarTodos();
            return listar.ToList();
        }
    }
}
