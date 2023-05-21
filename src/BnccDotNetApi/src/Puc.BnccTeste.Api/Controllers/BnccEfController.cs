using Microsoft.AspNetCore.Mvc;
using Puc.BnccTeste.Service.Interface;
using Puc.BnccTeste.Api.DTOs;
using ClosedXML.Excel;
using ClosedXML;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using Puc.BnccTeste.Domain.Entidade;
using System.Linq;
using Puc.BnccTeste.Domain.ObjetoValor;

namespace Puc.BnccTeste.Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class BnccEfController : Controller
    {
        private readonly IBnccMatematicaEfService _matematica;
        private readonly IBnccLinguaPortuguesaEfService _portugues;
        private readonly IUsuarioService _usuario;

        public BnccEfController(
            IBnccMatematicaEfService matematica, 
            IBnccLinguaPortuguesaEfService portugues,
            IUsuarioService usuario)
        {
            _matematica = matematica;
            _portugues = portugues;
            _usuario = usuario;
        }

        [HttpPost("/api/ListarAnosDaMateria")]
        public JsonResult ListarAnosDaMateria([FromQuery] IEnumerable<string> materia,bool todos , bool primeiroAno, bool segundoAno , bool terceiroAno , bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            try
            {        
                dynamic result;
                List<dynamic> lista = new List<dynamic>();

                if (materia.Any())
                {
                    if (materia.Contains("matematica"))
                    {
                        result = _matematica.ListarAnosMatematica(materia, todos, primeiroAno, segundoAno, terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno)
                            .Select(x => new BnccMatematicaEfDTO
                            {
                                Column1 = x.Column1,
                                Componente = x.Componente,
                                AnoFaixa = x.AnoFaixa,
                                UnidadesTematicas = x.UnidadesTematicas,
                                ObjetosConhecimento = x.ObjetosConhecimento,
                                Habilidades = x.Habilidades,
                                CodHab = x.CodHab,
                                DescricaoCod = x.DescricaoCod
                            });

                        lista.Add(result);
                    }

                    if (materia.Contains("portugues"))
                    {
                        result = _portugues.ListarAnosPortugues(materia, todos, primeiroAno, segundoAno, terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno)
                            .Select(x => new BnccLinguaPortuguesaEfDTO
                            {
                                Column1 = x.Column1,
                                Componente = x.Componente,
                                AnoFaixa = x.AnoFaixa,
                                CampoAtuacao = x.CampoAtuacao,
                                PraticasLinguagem = x.PraticasLinguagem,
                                ObjetosConhecimento = x.ObjetosConhecimento,
                                Habilidades = x.Habilidades,
                                CodHab = x.CodHab,
                                DescricaoCod = x.DescricaoCod
                            });

                        lista.Add(result);
                    }
                        
                    return Json(lista);                    
                }

                return Json("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return Json("Ocorreu algo inesperado!");
            }
        }

        [HttpGet("/api/Excel")]
        public ActionResult Excel([FromQuery] IEnumerable<string> materia, bool todos, bool primeiroAno, bool segundoAno, bool terceiroAno, bool quartoAno, bool quintoAno, bool sextoAno, bool setimoAno, bool oitavoAno, bool nonoAno)
        {
            using (var workbook = new XLWorkbook())
            {
                try
                {
                    List<string> listaMateria = materia.ToList();
                    var result = new List<dynamic>();
                    if (listaMateria.Any())
                    {
                        if (listaMateria.Contains("matematica"))
                        {
                            var listaMat = _matematica.ListarAnosMatematica(new List<string>() { listaMateria.ToString() }, todos, primeiroAno, segundoAno, terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno);
                            foreach (var item in listaMat)
                            {
                                var matematica = new BnccMatematicaEfDTO();
                                matematica.Column1 = item.Column1;
                                matematica.Componente = item.Componente;
                                matematica.AnoFaixa = item.AnoFaixa;
                                matematica.UnidadesTematicas = item.UnidadesTematicas;
                                matematica.ObjetosConhecimento = item.ObjetosConhecimento;
                                matematica.Habilidades = item.Habilidades;
                                matematica.CodHab = item.CodHab;
                                matematica.DescricaoCod = item.DescricaoCod;

                                result.Add(matematica);
                            }

                            #region PlanilhaMat

                            var planilha = workbook.AddWorksheet("Matematica");
                            var linha = 3;

                            //Caso ocorra algum erro copie o caminho da imagem na pasta img em Puc.BnccTeste.Api/Img e substitua na variável bnccImage
                            var bnccImage = "h:\\root\\home\\gustavaosmarter-001\\www\\Bncc1\\BannerBNcc.png";                   
                            var titulo = "Matemática";

                            planilha.AddPicture(bnccImage).MoveTo(planilha.Cell(1, 1)).ScaleWidth(1.045, true);

                            planilha.Cell(2, 1).Value = titulo;
                            planilha.Cell(2, 1).Worksheet.Range("A2", "G2").Merge();

                            planilha.Cell(linha, 1).Value = "Componente";
                            planilha.Cell(linha, 2).Value = "AnoFaixa";
                            planilha.Cell(linha, 3).Value = "Unidades Temáticas";
                            planilha.Cell(linha, 4).Value = "ObjetosConhecimento";
                            planilha.Cell(linha, 5).Value = "Habilidades";
                            planilha.Cell(linha, 6).Value = "CodHab";
                            planilha.Cell(linha, 7).Value = "DescricaoCod";

                            //Style     
                            #region Font
                            planilha.Cell(2, 1).Style
                                .Font.SetFontSize(16)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 1).Style
                                 .Font.SetFontSize(13)
                                 .Font.SetFontName("Calibri")
                                 .Font.SetBold(true)
                                 .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 2).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 3).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 4).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 5).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 6).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 7).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            #endregion

                            #region Alinhamento
                            planilha.Cell(2, 1).Style
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            planilha.Cell(linha, 1).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 2).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 3).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 4).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 5).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 6).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 7).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            #endregion

                            #region Alt/Larg
                            planilha.Cell(1, 1).Worksheet.Row(1).Height = 118;
                            planilha.Cell(2, 1).Worksheet.Row(2).Height = 35;
                            planilha.Cell(linha, 1).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 2).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 3).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 4).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 5).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 6).Worksheet.Row(3).Height = 35;
                            planilha.Cell(linha, 7).Worksheet.Row(3).Height = 35;

                            planilha.Cell(linha, 1).Worksheet.Column(1).Width = 20;
                            planilha.Cell(linha, 2).Worksheet.Column(2).Width = 20;
                            planilha.Cell(linha, 3).Worksheet.Column(3).Width = 35;
                            planilha.Cell(linha, 4).Worksheet.Column(4).Width = 45;
                            planilha.Cell(linha, 5).Worksheet.Column(5).Width = 55;
                            planilha.Cell(linha, 6).Worksheet.Column(6).Width = 20;
                            planilha.Cell(linha, 7).Worksheet.Column(7).Width = 70;
                            #endregion

                            #region Border
                            planilha.Cell(2, 1).Style
                                  .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                  .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                  .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                  .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 1).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 2).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 3).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 4).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 5).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 6).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 7).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            #endregion

                            #region Bg-Color
                            planilha.Cell(2, 1).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 1).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 2).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 3).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 4).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 5).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 6).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 7).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            #endregion

                            foreach (var lista in result)
                            {
                                linha++;
                                planilha.Cell(linha, 1).Value = lista.Componente;
                                planilha.Cell(linha, 2).Value = lista.AnoFaixa;
                                planilha.Cell(linha, 3).Value = lista.UnidadesTematicas;
                                planilha.Cell(linha, 4).Value = lista.ObjetosConhecimento;
                                planilha.Cell(linha, 5).Value = lista.Habilidades;
                                planilha.Cell(linha, 6).Value = lista.CodHab;
                                planilha.Cell(linha, 7).Value = lista.DescricaoCod;

                                //Style
                                #region FontSize/Style
                                planilha.Cell(linha, 1).Style
                                    .Font.SetFontSize(13)
                                    .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 2).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 3).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 4).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 5).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 6).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 7).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");

                                #endregion

                                #region Alinhamento
                                planilha.Cell(linha, 1).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 2).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 3).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 4).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 5).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 6).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 7).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                #endregion

                                #region QuebraDeLinha
                                planilha.Cell(linha, 1).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 2).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 3).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 4).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 5).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 6).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 7).Style.Alignment.WrapText = true;
                                #endregion

                                #region Altura
                                planilha.Cell(linha, 1).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 2).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 3).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 4).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 5).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 6).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 7).Worksheet.Row(linha).Height = 200;
                                #endregion

                                #region Bg-Color
                                planilha.Cell(linha, 4).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 5).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 6).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 7).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                #endregion

                                #region Border
                                planilha.Cell(linha, 1).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 2).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 3).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 4).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 5).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 6).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 7).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                #endregion

                            }
                            result.Clear();
                            #endregion
                        }

                        if (listaMateria.Contains("portugues"))
                        {
                            var listaPort = _portugues.ListarAnosPortugues(new List<string>() { listaMateria.ToString() }, todos, primeiroAno, segundoAno, terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno);
                            foreach (var item in listaPort)
                            {
                                var portugues = new BnccLinguaPortuguesaEfDTO();
                                portugues.Column1 = item.Column1;
                                portugues.Componente = item.Componente;
                                portugues.AnoFaixa = item.AnoFaixa;
                                portugues.CampoAtuacao = item.CampoAtuacao;
                                portugues.PraticasLinguagem = item.PraticasLinguagem;
                                portugues.ObjetosConhecimento = item.ObjetosConhecimento;
                                portugues.Habilidades = item.Habilidades;
                                portugues.CodHab = item.CodHab;
                                portugues.DescricaoCod = item.DescricaoCod;

                                result.Add(portugues);
                            }

                            #region PlanilhaPort
                            var planilha = workbook.AddWorksheet("Portugês");
                            var linha = 3;
                            var BnccImage = @"h:\root\home\gustavaosmarter-001\www\Bncc1\BannerBNcc.png";
                            var titulo = "Português";
                            planilha.AddPicture(BnccImage).MoveTo(planilha.Cell(1, 1)).ScaleWidth(1.221, true);

                            planilha.Cell(2, 1).Value = titulo;
                            planilha.Cell(2, 1).Worksheet.Range("A2", "H2").Merge();

                            planilha.Cell(linha, 1).Value = "Componente";
                            planilha.Cell(linha, 2).Value = "AnoFaixa";
                            planilha.Cell(linha, 3).Value = "Campo Atuação";
                            planilha.Cell(linha, 4).Value = "Práticas de Linguagem";
                            planilha.Cell(linha, 5).Value = "ObjetosConhecimento";
                            planilha.Cell(linha, 6).Value = "Habilidades";
                            planilha.Cell(linha, 7).Value = "CodHab";
                            planilha.Cell(linha, 8).Value = "DescricaoCod";

                            //Style     
                            #region Font
                            planilha.Cell(2, 1).Style
                                .Font.SetFontSize(16)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 1).Style
                                 .Font.SetFontSize(13)
                                 .Font.SetFontName("Calibri")
                                 .Font.SetBold(true)
                                 .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 2).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 3).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 4).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 5).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 6).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 7).Style
                                .Font.SetFontSize(13)
                                .Font.SetFontName("Calibri")
                                .Font.SetBold(true)
                                .Font.SetFontColor(XLColor.White);
                            planilha.Cell(linha, 8).Style
                               .Font.SetFontSize(13)
                               .Font.SetFontName("Calibri")
                               .Font.SetBold(true)
                               .Font.SetFontColor(XLColor.White);
                            #endregion

                            #region Alinhamento
                            planilha.Cell(2, 1).Style
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                            planilha.Cell(linha, 1).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 2).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 3).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 4).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 5).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 6).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 7).Style
                                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            planilha.Cell(linha, 8).Style
                               .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                               .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            #endregion

                            #region Alt/Larg
                            planilha.Cell(1, 1).Worksheet.Row(1).Height = 118;
                            planilha.Cell(2, 1).Worksheet.Row(2).Height = 35;
                            planilha.Cell(linha, 1).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 2).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 3).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 4).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 5).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 6).Worksheet.Row(linha).Height = 75;
                            planilha.Cell(linha, 7).Worksheet.Row(linha).Height = 35;
                            planilha.Cell(linha, 8).Worksheet.Row(linha).Height = 75;

                            planilha.Cell(linha, 1).Worksheet.Column(1).Width = 20;
                            planilha.Cell(linha, 2).Worksheet.Column(2).Width = 20;
                            planilha.Cell(linha, 3).Worksheet.Column(3).Width = 35;
                            planilha.Cell(linha, 4).Worksheet.Column(4).Width = 45;
                            planilha.Cell(linha, 5).Worksheet.Column(5).Width = 45;
                            planilha.Cell(linha, 6).Worksheet.Column(6).Width = 55;
                            planilha.Cell(linha, 7).Worksheet.Column(7).Width = 20;
                            planilha.Cell(linha, 8).Worksheet.Column(8).Width = 70;
                            #endregion


                            #region Border
                            planilha.Cell(2, 1).Style
                                  .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                  .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                  .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                  .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 1).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 2).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 3).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 4).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 5).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 6).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 7).Style
                                .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            planilha.Cell(linha, 8).Style
                               .Border.SetRightBorder(XLBorderStyleValues.Medium)
                               .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                               .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                               .Border.SetTopBorder(XLBorderStyleValues.Medium);
                            #endregion

                            #region Bg-Color
                            planilha.Cell(2, 1).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 1).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 2).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 3).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 4).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 5).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 6).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 7).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            planilha.Cell(linha, 8).Style.Fill.SetBackgroundColor(XLColor.DodgerBlue);
                            #endregion

                            foreach (var lista in result)
                            {
                                linha++;
                                planilha.Cell(linha, 1).Value = lista.Componente;
                                planilha.Cell(linha, 2).Value = lista.AnoFaixa;
                                planilha.Cell(linha, 3).Value = lista.CampoAtuacao;
                                planilha.Cell(linha, 4).Value = lista.PraticasLinguagem;
                                planilha.Cell(linha, 5).Value = lista.ObjetosConhecimento;
                                planilha.Cell(linha, 6).Value = lista.Habilidades;
                                planilha.Cell(linha, 7).Value = lista.CodHab;
                                planilha.Cell(linha, 8).Value = lista.DescricaoCod;

                                //Style
                                #region FontSize/Style
                                planilha.Cell(linha, 1).Style
                                    .Font.SetFontSize(13)
                                    .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 2).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 3).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 4).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 5).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 6).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 7).Style
                                   .Font.SetFontSize(13)
                                   .Font.SetFontName("Calibri");
                                planilha.Cell(linha, 8).Style
                                  .Font.SetFontSize(13)
                                  .Font.SetFontName("Calibri");

                                #endregion

                                #region Alinhamento
                                planilha.Cell(linha, 1).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 2).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 3).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 4).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 5).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 6).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 7).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                planilha.Cell(linha, 8).Style
                                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                #endregion

                                #region QuebraDeLinha
                                planilha.Cell(linha, 1).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 2).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 3).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 4).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 5).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 6).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 7).Style.Alignment.WrapText = true;
                                planilha.Cell(linha, 8).Style.Alignment.WrapText = true;
                                #endregion

                                #region Altura
                                planilha.Cell(linha, 1).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 2).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 3).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 4).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 5).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 6).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 7).Worksheet.Row(linha).Height = 200;
                                planilha.Cell(linha, 8).Worksheet.Row(linha).Height = 200;
                                #endregion

                                #region Bg-Color
                                planilha.Cell(linha, 4).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 5).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 6).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 7).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                planilha.Cell(linha, 8).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                                #endregion

                                #region Border
                                planilha.Cell(linha, 1).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 2).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 3).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 4).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 5).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 6).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 7).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                planilha.Cell(linha, 8).Style
                                    .Border.SetRightBorder(XLBorderStyleValues.Medium)
                                    .Border.SetBottomBorder(XLBorderStyleValues.Medium)
                                    .Border.SetLeftBorder(XLBorderStyleValues.Medium)
                                    .Border.SetTopBorder(XLBorderStyleValues.Medium);
                                #endregion
                            }
                            result.Clear();
                            #endregion
                        }
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest("Algo inesperado ocorreu!");
                }
              
                using (var stream = new MemoryStream())
                {
                    try
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"BNCC.xlsx");
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        [HttpPost("/api/Login")]
        public dynamic Login([FromBody] LoginUsuario usuario)
        {            
            try
            {
                if (usuario != null) 
                { 
                    var result = _usuario.Login(usuario);

                    if (result != null)                    
                        return Ok(result);                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao logar contate o administrador do sistema");
            }
            return Ok("Insira um email e senha");
        }

        [HttpPost("/api/Registrar")]
        public dynamic Registrar([FromBody] Usuario usuario)
        {         
            try
            {                 
                var result = _usuario.Registrar(usuario);

                if(result != null)
                    return Ok(result);
               
            }
            catch (Exception ex)
            {
               return BadRequest("Erro inesperado contate o administrador do sistema!" + ex);
            }

            return Ok("Erro inesperado");
        }
    }
}
