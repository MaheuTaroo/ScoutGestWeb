#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2c268e66c52491983d4245e2706a8513bb022695"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_InserirEscuteiro_Detalhes), @"mvc.1.0.view", @"/Views/InserirEscuteiro/Detalhes.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c268e66c52491983d4245e2706a8513bb022695", @"/Views/InserirEscuteiro/Detalhes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_InserirEscuteiro_Detalhes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ScoutGestWeb.Models.InserirEscuteiroViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
  
    ViewData["Title"] = "Detalhes - Escuteiros";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"tabledeth\">\r\n    <h1>Detalhes</h1>\r\n    <div class=\"rowdeth\">\r\n        <div class=\"columndet\">\r\n            <dl class=\"row\">\r\n");
            WriteLiteral("                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 21 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 24 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayNameFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Secção:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 33 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Seccao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Estado:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 39 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Idade:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 45 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Idade));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 48 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayNameFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 51 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 51 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
                                                        Write(Html.DisplayFor(model => model.Morada2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Código-postal:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 57 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.CodPostal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Grupo sanguíneo:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 63 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.GrupoSanguineo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Alergias:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 69 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Alergias));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Medicação:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 75 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Medicacao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Problemas:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 81 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Problemas));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Observações:\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 87 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
               Write(Html.DisplayFor(model => model.Observacoes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n        </div>\r\n        <div class=\"columndet\">\r\n            <div class=\"imagedeth\">\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 3402, "\"", 3449, 1);
#nullable restore
#line 93 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
WriteAttributeValue("", 3408, Html.DisplayFor(model => model.FotoDown), 3408, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        ");
#nullable restore
#line 99 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
   Write(Html.ActionLink("Edit", "Edit", new { /* id = Model.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" | ");
#nullable restore
#line 99 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
                                                                           Write(Html.ActionLink("Eliminar", "ElimGet", new { id = Model.ID}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" | \r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c268e66c52491983d4245e2706a8513bb02269511447", async() => {
                WriteLiteral("Voltar à listagem");
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
            WriteLiteral(@"
    </div>
</div>


<!--dividiste o ecra a meio?-->
<!--yeah, esse nao era o teu plano? tavas a tentar fazer isso nao era?-->
<!--em principio sim-->
<!--so what do u think? like the image probably could be better but i think the text is fine as in the placement-->
<!--a img ta fixe-->
<!--then is everything right with the page?-->
<!--hold up real quick
    start now
    semmingly yes-->
<!--next page tell me-->
<!--tenta so centrar o texto 1º-->
<!--desta pag? oki-->
<!--posso tirar a classe table daqui?-->
<!--n consegues por mais p a direita c a tabela p aqui?-->
<!--i could try moving the table, tbh i wasn't thinking that-->
<!--eu tinha te dito p centrar?-->
<!--sim-->
<!--wasnt meaning that, meant por um bocado p a direita-->
<!--oh then thats easier meto um padding e there-->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ScoutGestWeb.Models.InserirEscuteiroViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
