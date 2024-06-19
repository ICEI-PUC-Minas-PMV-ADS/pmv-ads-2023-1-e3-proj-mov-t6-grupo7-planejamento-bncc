using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Api.DTOs
{
    public class BnccMatematicaEfDTO
    {
        public int Column1 { get; set; }

        public string Componente { get; set; } = null!;

        public string AnoFaixa { get; set; } = null!;

        public string UnidadesTematicas { get; set; } = null!;

        public string ObjetosConhecimento { get; set; } = null!;

        public string Habilidades { get; set; } = null!;

        public string? CodHab { get; set; }

        public string DescricaoCod { get; set; } = null!;
       
    }
}
