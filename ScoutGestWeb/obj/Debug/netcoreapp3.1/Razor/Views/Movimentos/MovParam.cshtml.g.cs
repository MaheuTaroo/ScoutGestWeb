#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "015081a347ab9b213104aebdace772582af2c2c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movimentos_MovParam), @"mvc.1.0.view", @"/Views/Movimentos/MovParam.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"015081a347ab9b213104aebdace772582af2c2c9", @"/Views/Movimentos/MovParam.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dabf5d1fdd271bd0b8268ec1a44282d3a7cebae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Movimentos_MovParam : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
  
    ViewData["Title"] = "MovParam";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>MovParam</h1>\r\n\r\n");
#nullable restore
#line 8 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
 using (Html.BeginForm("MovParam", "Movimentos"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div>
        <h3>Data</h3><br />
        <label>Início: <input type=""datetime-local"" name=""dataInicio"" /></label><br />
        <label>Fim: <input type=""datetime-local"" name=""dataFim"" /></label>
    </div>
    <div>
        <h3>Caixas</h3><br />
        <label>
            Início:
            <select name=""caixaInicio"">
");
#nullable restore
#line 20 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.caixas)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c94163", async() => {
#nullable restore
#line 22 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 23 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label><br />\r\n        <label>\r\n            Fim:\r\n            <select name=\"caixaFim\">\r\n");
#nullable restore
#line 29 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.caixas)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c95871", async() => {
#nullable restore
#line 31 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 32 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label>\r\n    </div>\r\n    <div>\r\n        <h3>Tipos de pagamento</h3><br />\r\n        <label>\r\n            Início:\r\n            <select name=\"tipoPagInicio\">\r\n");
#nullable restore
#line 41 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.pags)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c97651", async() => {
#nullable restore
#line 43 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 44 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label><br />\r\n        <label>\r\n            Fim:\r\n            <select name=\"tipoPagFim\">\r\n");
#nullable restore
#line 50 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.pags)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c99359", async() => {
#nullable restore
#line 52 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 53 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label>\r\n    </div>\r\n    <div>\r\n        <h3>Atividades</h3><br />\r\n        <label>\r\n            Início:\r\n            <select name=\"ativInicio\">\r\n");
#nullable restore
#line 62 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.ativs)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c911129", async() => {
#nullable restore
#line 64 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 65 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label><br />\r\n        <label>\r\n            Fim:\r\n            <select name=\"ativFim\">\r\n");
#nullable restore
#line 71 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.ativs)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c912836", async() => {
#nullable restore
#line 73 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 74 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label>\r\n    </div>\r\n    <div>\r\n        <h3>Secções</h3><br />\r\n        <label>\r\n            Início:\r\n            <select name=\"seccaoInicio\">\r\n");
#nullable restore
#line 83 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.seccoes)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c914608", async() => {
#nullable restore
#line 85 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 86 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label><br />\r\n        <label>\r\n            Fim:\r\n            <select name=\"seccaoFim\">\r\n");
#nullable restore
#line 92 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                 foreach (string s in ViewBag.seccoes)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "015081a347ab9b213104aebdace772582af2c2c916319", async() => {
#nullable restore
#line 94 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                       Write(s);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 95 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </label>\r\n    </div>\r\n");
#nullable restore
#line 99 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Movimentos\MovParam.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591