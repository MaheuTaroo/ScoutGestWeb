#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d37cffec3bc9db298e5b825368124c69d31815e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Caixas_Index), @"mvc.1.0.view", @"/Views/Caixas/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d37cffec3bc9db298e5b825368124c69d31815e9", @"/Views/Caixas/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83a0dfa3088d4344f1be9ceef8152525895fcb3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Caixas_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.CaixaViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NovaCaixa", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
  
    ViewData["Title"] = "Caixas de pagamentos - Tesouraria";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <aside class=\"sidenav\">\r\n        <ul>\r\n            <li class=\"linksMovs\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d37cffec3bc9db298e5b825368124c69d31815e93722", async() => {
                WriteLiteral("Nova caixa");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </li>\r\n        </ul>\r\n    </aside>\r\n</div>\r\n<div class=\"mainBody\">\r\n");
#nullable restore
#line 17 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
     if (!User.IsInRole("Comum"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1>Caixas</h1>\r\n");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
        if (TempData["msg"] != null)
        {
            if (TempData["msgKeep"].ToString().Contains("Ocorreu um erro"))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"bg-danger rounded text-center\">\r\n                    <b>TempData[\"msg\"]</b>\r\n                </div>\r\n");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"bg-success rounded text-center\">\r\n                    <b>TempData[\"msg\"]</b>\r\n                </div>\r\n");
#nullable restore
#line 33 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <table class=\"table\">\r\n            <thead>\r\n                <tr>\r\n                    <th>\r\n                        ");
#nullable restore
#line 39 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 42 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 45 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </th>
                    <th>
                        Responsável
                    </th>
                    <th>
                        Saldo
                    </th>
                    <th>Opções</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 57 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 61 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 64 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 67 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 70 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Responsavel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 73 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Saldo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 76 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.ActionLink("Editar", "Editar", new { id=item.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            ");
#nullable restore
#line 77 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                       Write(Html.ActionLink("Eliminar", "Eliminar", new { id=item.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 80 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n");
#nullable restore
#line 83 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"text-center\">\r\n            <h1>A sua caixa</h1>\r\n            <div style=\"display: flex; justify-content: center; align-items: center\">\r\n                <table>\r\n");
#nullable restore
#line 90 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <th>ID de caixa:</th>\r\n                            <td>");
#nullable restore
#line 94 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <th>Nome:</th>\r\n                            <td>");
#nullable restore
#line 98 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <th>Grupo atribuído:</th>\r\n                            <td>");
#nullable restore
#line 102 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <th>Responsável atribuído:</th>\r\n                            <td>");
#nullable restore
#line 106 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Responsavel));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <th>Saldo de caixa:</th>\r\n                            <td>");
#nullable restore
#line 110 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Saldo));

#line default
#line hidden
#nullable disable
            WriteLiteral("€</td>\r\n                        </tr>\r\n");
#nullable restore
#line 112 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </table>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 116 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Caixas\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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