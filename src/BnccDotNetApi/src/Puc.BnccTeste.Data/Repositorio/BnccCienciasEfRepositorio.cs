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
    public class BnccCienciasEfRepositorio : RepositorioBase<BnccCienciasEf>, IBnccCienciasEfRepositorio
    {
        public BnccCienciasEfRepositorio(Contexto contexto)
            : base(contexto)
        {
        }
    }
}
