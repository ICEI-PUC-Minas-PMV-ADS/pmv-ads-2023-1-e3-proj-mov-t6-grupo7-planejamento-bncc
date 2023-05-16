using System;
using System.Collections.Generic;

namespace Puc.BnccTeste.Domain.Entidade;

public partial class BnccLinguaInglesaEf
{
    public int Column1 { get; set; }

    public string Componente { get; set; } = null!;

    public string AnoFaixa { get; set; } = null!;

    public string Eixo { get; set; } = null!;

    public string UnidadesTematicas { get; set; } = null!;

    public string ObjetosConhecimento { get; set; } = null!;

    public string Habilidades { get; set; } = null!;

    public string CodHab { get; set; } = null!;

    public string DescricaoCod { get; set; } = null!;

    public byte PrimeiroEf { get; set; }

    public byte SegundoEf { get; set; }

    public byte TerceiroEf { get; set; }

    public byte QuartoEf { get; set; }

    public byte QuintoEf { get; set; }

    public bool SextoEf { get; set; }

    public bool SetimoEf { get; set; }

    public bool OitavoEf { get; set; }

    public bool NonoEf { get; set; }
}
