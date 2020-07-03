#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76a07bec3f7a148be548ba546306b7ace4249a53"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movimentos_Analises_MovimentosCaixa), @"mvc.1.0.view", @"/Views/Movimentos/Analises/MovimentosCaixa.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\_ViewImports.cshtml"
using ScoutGestWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\_ViewImports.cshtml"
using ScoutGestWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76a07bec3f7a148be548ba546306b7ace4249a53", @"/Views/Movimentos/Analises/MovimentosCaixa.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Movimentos_Analises_MovimentosCaixa : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.MovimentoViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
  
    Layout = "_PdfLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<table class=""table"" style=""border: 2px solid black; margin: auto 0 auto 0;"">
    <thead>
        <tr>
            <th>Caixa</th>
            <th>Documento</th>
            <th>Secção</th>
            <th>Tipo de movimento</th>
            <th>Username</th>
            <th>Data/Hora</th>
            <th>Valor</th>
            <th>Tipo de pagamento</th>
            <th>Descrição</th>
            <th>Atividade</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 22 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 25 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.IDCaixa));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 26 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.IDDocumento));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.Seccao));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 28 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.TipoMovimento));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 29 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.User.NormalizedUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 30 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.DataHora));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.Valor));

#line default
#line hidden
#nullable disable
            WriteLiteral("€</td>\r\n                <td>");
#nullable restore
#line 32 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.TipoPagamento));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.Descricao));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
               Write(Html.DisplayFor(modelItem => item.Atividade));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 36 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<br />\r\n<br />\r\n<div style=\"position: relative; font-size: 20px; margin: 0 25px 0 25px\">\r\n    <p style=\"float: right\">Despesas: ");
#nullable restore
#line 42 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
                                 Write(Model.ToList().Where(x => x.TipoMovimento == "Saída de tesouraria").Sum(x => x.Valor));

#line default
#line hidden
#nullable disable
            WriteLiteral("€</p>\r\n    <p style=\"float: left\">Receitas: ");
#nullable restore
#line 43 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\MovimentosCaixa.cshtml"
                                Write(Model.ToList().Where(x => x.TipoMovimento == "Entrada de tesouraria").Sum(x => x.Valor));

#line default
#line hidden
#nullable disable
            WriteLiteral("€</p>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.MovimentoViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
