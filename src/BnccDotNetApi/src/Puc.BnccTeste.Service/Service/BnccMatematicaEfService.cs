using Puc.BnccTeste.Domain.Entidade;
using Puc.BnccTeste.Infra.Data.Interface;
using Puc.BnccTeste.Service.Interface;
using System.Text.Json.Nodes;

namespace Puc.BnccTeste.Service.Service
{
    public class BnccMatematicaEfService : IBnccMatematicaEfService
    {
        private readonly IBnccMatematicaEfRepositorio _matRepo;
        public BnccMatematicaEfService(IBnccMatematicaEfRepositorio matRepo)
        {
            _matRepo = matRepo;
        }              

        public IEnumerable<BnccMatematicaEf> ListarAnosMatematica(IEnumerable<string> materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            var lista = _matRepo.ListarTodos().ToList();
            try
            {
                
                if(lista != null)
                { 
                    if(todos)                        
                        return lista;
                        
                    if(primeiroAno || segundoAno ||  terceiroAno || quartoAno || quintoAno || sextoAno || setimoAno || oitavoAno || nonoAno)
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

                    return lista;
                }                    
                              
            }
            catch (Exception ex)
            {

                return null;
            }

            return null;
        }

        public IEnumerable<BnccMatematicaEf> ListarTodos()
        {
            List<BnccMatematicaEf> lista = new List<BnccMatematicaEf>();
            try
            {
                lista = _matRepo.ListarTodos().ToList();               
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }

        public BnccMatematicaEf ObterPeloCodHab(string cod)
        {
            try
            {
                return _matRepo.ObterPeloCodHab(cod);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BnccMatematicaEf ObterPeloId(int id)
        {
            try
            {
                return _matRepo.ObterPeloId(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
