#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c29f535d3307b47cb983d99ad169112f426de5b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movimentos_Analises_RankingsAtivs), @"mvc.1.0.view", @"/Views/Movimentos/Analises/RankingsAtivs.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c29f535d3307b47cb983d99ad169112f426de5b", @"/Views/Movimentos/Analises/RankingsAtivs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Movimentos_Analises_RankingsAtivs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.MovimentoViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml"
  
    ViewData["Title"] = "RankingsAtivs";
    Layout = "~/Views/Shared/_PdfLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>Atividade</th>\r\n            <th>Valor</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 15 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 17 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml"
           Write(Html.DisplayFor(modelItem => item.Atividade));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml"
           Write(Html.DisplayFor(modelItem => item.Valor));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\Analises\RankingsAtivs.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.MovimentoViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591