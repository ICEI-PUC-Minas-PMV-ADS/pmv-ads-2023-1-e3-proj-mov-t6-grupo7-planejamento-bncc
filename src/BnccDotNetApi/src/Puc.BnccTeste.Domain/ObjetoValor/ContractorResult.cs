using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Domain.ObjetoValor
{
    [Serializable]
    public class ContractorResult : IContractorResult
    {        
        public string Message { get; set; }
        public bool AcaoValida { get; set; }
        public object Data { get; set; }
        
    }
    public interface IContractorResult 
    {
        string Message { get; set; }
        bool AcaoValida { get; set; }
        public object Data { get; set; }
    }
}
