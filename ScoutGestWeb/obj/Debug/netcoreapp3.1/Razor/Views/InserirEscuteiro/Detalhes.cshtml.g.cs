#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2a44863e62b51fd1d761f0740fcae5458944a4e9"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a44863e62b51fd1d761f0740fcae5458944a4e9", @"/Views/InserirEscuteiro/Detalhes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d270e562d411643ae575e95155c374c628fd947", @"/Views/_ViewImports.cshtml")]
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
  
    ViewData["Title"] = "Detalhes";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Detalhes</h1>\r\n\r\n<div>\r\n    <h4>AtividadeViewModel</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <img");
            BeginWriteAttribute("src", " src=\"", 209, "\"", 256, 1);
#nullable restore
#line 13 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
WriteAttributeValue("", 215, Html.DisplayFor(model => model.FotoDown), 215, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n        <!--<dt class = \"col-sm-2\">\r\n        ");
#nullable restore
#line 15 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
   Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class = \"col-sm-10\">\r\n        ");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
   Write(Html.DisplayFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>-->\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayNameFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Secção:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Seccao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Estado:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Idade:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Idade));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 51 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayNameFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 54 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
                                                Write(Html.DisplayFor(model => model.Morada2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Código-postal:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.CodPostal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Grupo sanguíneo:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.GrupoSanguineo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Alergias:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Alergias));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Medicação:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 78 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Medicacao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Problemas:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 84 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Problemas));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Observações:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 90 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
       Write(Html.DisplayFor(model => model.Observacoes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
#nullable restore
#line 95 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\InserirEscuteiro\Detalhes.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { /* id = Model.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2a44863e62b51fd1d761f0740fcae5458944a4e910758", async() => {
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
            WriteLiteral("\r\n</div>\r\n");
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
