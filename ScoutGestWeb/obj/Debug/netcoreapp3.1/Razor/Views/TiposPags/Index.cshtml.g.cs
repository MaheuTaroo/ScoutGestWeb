#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f20bdc91d5969494561ee8c501e3e9f300b9e5b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TiposPags_Index), @"mvc.1.0.view", @"/Views/TiposPags/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f20bdc91d5969494561ee8c501e3e9f300b9e5b", @"/Views/TiposPags/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_TiposPags_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.TiposPagsViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NovoPagamento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
  
    ViewData["Title"] = "Informação do pagamento - Tesouraria";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"text-white\">\r\n    <aside class=\"sidenav\">\r\n        <br />\r\n");
#nullable restore
#line 8 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
         if (User.IsInRole("Administração de Agrupamento"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div style=\"padding: 3px\">\r\n                <h4>Pagamentos</h4>\r\n                <hr />\r\n                <ul>\r\n                    <li class=\"linksMovs\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7f20bdc91d5969494561ee8c501e3e9f300b9e5b4132", async() => {
                WriteLiteral("Novo pagamento");
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
            WriteLiteral("\r\n                    </li>\r\n                </ul>\r\n                <hr />\r\n            </div>\r\n");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </aside>\r\n</div>\r\n<div class=\"mainBody\">\r\n    <h1>Pagamentos</h1>\r\n");
#nullable restore
#line 25 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
     if (TempData["msg"] != null)
    {
        if (TempData["msg"].ToString().Contains("Ocorreu um erro"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"bg-danger rounded text-center\">\r\n                <b>");
#nullable restore
#line 30 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
              Write(TempData["msg"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </div>\r\n");
#nullable restore
#line 32 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"bg-success rounded text-center\">\r\n                <b>");
#nullable restore
#line 36 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
              Write(TempData["msg"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </div>\r\n");
#nullable restore
#line 38 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>ID do pagamento</th>\r\n                <th>Pagamento</th>\r\n                <th>Opções</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 49 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 53 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.IDPagamento));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 56 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Pagamento));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 59 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
                   Write(Html.ActionLink("Editar", "Edit", new { id = item.IDPagamento }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                        ");
#nullable restore
#line 60 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
                   Write(Html.ActionLink("Eliminar", "EliminarGet", new { id = item.IDPagamento }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 63 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\TiposPags\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.TiposPagsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
