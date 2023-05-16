using System;
using System.Collections.Generic;

namespace Puc.BnccTeste.Domain.Entidade;

public partial class EscutaFalaEdInf
{
    public int Column1 { get; set; }

    public string? CampoExp { get; set; }

    public string? FaixaEtaria { get; set; }

    public string? Obj { get; set; }

    public string? CodApr { get; set; }

    public string? DescricaoCod { get; set; }

    public byte? IdadeAnosInicial { get; set; }

    public byte? IdadeMesesInicial { get; set; }

    public byte? IdadeAnosFinal { get; set; }

    public byte? IdadeMesesFinal { get; set; }
}
