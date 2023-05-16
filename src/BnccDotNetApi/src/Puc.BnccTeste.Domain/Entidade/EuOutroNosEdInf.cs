using System;
using System.Collections.Generic;

namespace Puc.BnccTeste.Domain.Entidade;

public partial class EuOutroNosEdInf
{
    public int Column1 { get; set; }

    public string CampoExp { get; set; } = null!;

    public string FaixaEtaria { get; set; } = null!;

    public string Obj { get; set; } = null!;

    public string CodApr { get; set; } = null!;

    public string DescricaoCod { get; set; } = null!;

    public byte IdadeAnosInicial { get; set; }

    public byte? IdadeMesesInicial { get; set; }

    public byte IdadeAnosFinal { get; set; }

    public byte IdadeMesesFinal { get; set; }
}
