#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d30e7c59cd9286423f9cd88cc377293bcb92d80"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movimentos_Analises_Caixas), @"mvc.1.0.view", @"/Views/Movimentos/Analises/Caixas.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d30e7c59cd9286423f9cd88cc377293bcb92d80", @"/Views/Movimentos/Analises/Caixas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83a0dfa3088d4344f1be9ceef8152525895fcb3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Movimentos_Analises_Caixas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.CaixaViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
  
    ViewData["Title"] = "Caixas";
    Layout = "~/Views/Shared/_PdfLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>ID</th>\r\n            <th>Nome</th>\r\n            <th>Grupo</th>\r\n            <th>Responsável</th>\r\n            <th>Saldo</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 21 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 22 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
           Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 23 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
           Write(Html.DisplayFor(modelItem => item.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 24 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
           Write(Html.DisplayFor(modelItem => item.Responsavel));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 25 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
           Write(Html.DisplayFor(modelItem => item.Saldo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\Caixas.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.CaixaViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591