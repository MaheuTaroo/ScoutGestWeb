#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3879ef9665adccdc9cf8f204772f63186e81fef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Escuteiros_Eliminar), @"mvc.1.0.view", @"/Views/Escuteiros/Eliminar.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3879ef9665adccdc9cf8f204772f63186e81fef", @"/Views/Escuteiros/Eliminar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Escuteiros_Eliminar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ScoutGestWeb.Models.EscuteirosViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EliminarPost", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "Post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
  
    ViewData["Title"] = "Eliminar escuteiro - Escuteiros";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"bg-danger rounded\">");
#nullable restore
#line 5 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                          Write(Html.ValidationSummary(false));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<h3>Tem a certeza que quer eliminar este escuteiro?</h3>\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 10 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 11 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 12 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 13 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Totem));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\"> ");
#nullable restore
#line 14 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayNameFor(model => model.Cargos));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 15 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Cargos));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Número(s) de telefone: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 17 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.NumTelefone));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 19 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Grupo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 21 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Morada));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 21 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                                                                 Write(Html.DisplayFor(model => model.Morada2));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Código-postal: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 23 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.CodPostal));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 24 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Localidade));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 25 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Localidade));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Grupo sanguíneo: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.GrupoSanguineo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 28 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Alergias));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 29 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Alergias));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Medicação: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 31 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Medicacao));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 32 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Problemas));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 33 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Problemas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Observações: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 35 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Observacoes));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Secção: </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 37 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Seccao));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 38 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 39 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">");
#nullable restore
#line 40 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                        Write(Html.DisplayNameFor(model => model.Idade));

#line default
#line hidden
#nullable disable
            WriteLiteral(": </dt>\r\n        <dd class=\"col-sm-10\">");
#nullable restore
#line 41 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                         Write(Html.DisplayFor(model => model.Idade));

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt class=\"col-sm-2\">Fotografia: </dt>\r\n        <dd class=\"col-sm-10\"><img");
            BeginWriteAttribute("src", " src=\"", 2768, "\"", 2815, 1);
#nullable restore
#line 43 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
WriteAttributeValue("", 2774, Html.DisplayFor(model => model.FotoDown), 2774, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></dd>\r\n    </dl>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c3879ef9665adccdc9cf8f204772f63186e81fef13741", async() => {
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Eliminar\" class=\"btn btn-danger\" /> |\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c3879ef9665adccdc9cf8f204772f63186e81fef14091", async() => {
                    WriteLiteral("Cancelar");
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
                WriteLiteral("\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 45 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Escuteiros\Eliminar.cshtml"
                                                    WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ScoutGestWeb.Models.EscuteirosViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
