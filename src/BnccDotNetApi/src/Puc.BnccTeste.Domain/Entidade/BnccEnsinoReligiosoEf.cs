using System;
using System.Collections.Generic;

namespace Puc.BnccTeste.Domain.Entidade;

public class BnccEnsinoReligiosoEf
{
    public int Column1 { get; set; }

    public string Componente { get; set; } = null!;

    public string AnoFaixa { get; set; } = null!;

    public string UnidadesTematicas { get; set; } = null!;

    public string ObjetosConhecimento { get; set; } = null!;

    public string Habilidades { get; set; } = null!;

    public string CodHab { get; set; } = null!;

    public string DescricaoCod { get; set; } = null!;

    public bool PrimeiroEf { get; set; }

    public bool SegundoEf { get; set; }

    public bool TerceiroEf { get; set; }

    public bool QuartoEf { get; set; }

    public bool QuintoEf { get; set; }

    public bool SextoEf { get; set; }

    public bool SetimoEf { get; set; }

    public bool OitavoEf { get; set; }

    public bool NonoEf { get; set; }
}
