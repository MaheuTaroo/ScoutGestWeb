#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5de81daf8f842db78262b25554c6ac8106339c12"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Grupos_Index), @"mvc.1.0.view", @"/Views/Grupos/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5de81daf8f842db78262b25554c6ac8106339c12", @"/Views/Grupos/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Grupos_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.GrupoViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NovoGrupo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
  
    ViewData["Title"] = "Grupos";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Informação geral sobre os grupos</h1>\r\n<!--Informação dos grupos-->\r\n\r\n");
#nullable restore
#line 9 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
 if (User.IsInRole("Administração de Agrupamento"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5de81daf8f842db78262b25554c6ac8106339c123875", async() => {
                WriteLiteral("Novo grupo");
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
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 14 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th style=\"background-color:lightgreen\">");
#nullable restore
#line 18 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
                                               Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th style=\"background-color:lightgreen\">");
#nullable restore
#line 19 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
                                               Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th style=\"background-color:lightgreen\">");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
                                               Write(Html.DisplayNameFor(model => model.Sigla));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th style=\"background-color:lightgreen\">Secção</th>\r\n            <th style=\"background-color:lightgreen\">Pseudónimo</th>\r\n            <th>Opções</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 27 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 31 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 34 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 37 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Sigla));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 40 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Seccao));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 43 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Pseudonimo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 46 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                    ");
#nullable restore
#line 47 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                    ");
#nullable restore
#line 48 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
               Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 51 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Grupos\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.GrupoViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
