#pragma checksum "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40389c0976d183052351387dd44870c45d3135b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Atividades_Index), @"mvc.1.0.view", @"/Views/Atividades/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40389c0976d183052351387dd44870c45d3135b5", @"/Views/Atividades/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d270e562d411643ae575e95155c374c628fd947", @"/Views/_ViewImports.cshtml")]
    public class Views_Atividades_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ScoutGestWeb.Models.AtividadeViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "InserirAtividade", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Detalhes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
  
    ViewData["Title"] = "Atividades";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Planeamento das atividades</h1>\r\n\r\n<p>\r\n    Aqui poderá ver as atividades que estão planeadas, ou que estão a ser realizadas\r\n    <br />");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40389c0976d183052351387dd44870c45d3135b54647", async() => {
                WriteLiteral("Criar nova atividade");
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
            WriteLiteral("\r\n</p>\r\n\r\n<!--<div class=\"main\">\r\n    -->\r\n<!--lmao este main n tava aqui a fzr nd\r\n    come with me-->\r\n<div style=\"overflow-y: scroll\">\r\n");
#nullable restore
#line 19 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"container-fluid\" style=\"padding: 0 0 0 0\">\r\n            <p>\r\n                <h2>\r\n                    ");
#nullable restore
#line 24 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h2>\r\n            </p>\r\n            <p>\r\n                <h5>\r\n                    De ");
#nullable restore
#line 29 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
                  Write(Html.DisplayFor(modelItem => item.DataInicio));

#line default
#line hidden
#nullable disable
            WriteLiteral(" até ");
#nullable restore
#line 29 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
                                                                     Write(Html.DisplayFor(modelItem => item.DataFim));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h5>\r\n            </p>\r\n            <!--<p>\r\n            A seguinte atividade foi realizada neste local: arroba Html.DisplayFor(modelItem => item.Local)  ,aqui apanhamos um gato pq ele era um gato\r\n            </p>-->\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40389c0976d183052351387dd44870c45d3135b57497", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 1178, "\"", 1233, 1);
#nullable restore
#line 36 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
WriteAttributeValue("", 1186, Html.DisplayFor(modelItem => item.IDAtividade), 1186, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <button class=\"btn btn-secondary\" type=\"submit\">Detalhes</button>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 40 "D:\GitHub\ScoutGestWeb\ScoutGestWeb\Views\Atividades\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
<!--</div>
<!--can i start?-->
<!--ya-->
<!--brb-->
<!--oki quando voltares vai ao site.css-->
<!--qrs comecar p testar qq cena?-->
<!--yea-->
<!--u can login-->
<!--so? how is it?-->
<!--tbh i think it's looking great, and i think besides adding stuff we don't need to change anything-->
<!--imo o visual sinto q ta um pouco pobre tho-->
<!--queres alterar as cores, ou ppreferes adicinar imagens?-->
<!--acho q p agr n e preciso imgs, e so mm a parte visual
    e o texto ta um bocado grande
    p ex o tamanho do cabecalho do logo tem demasiado padding-->
<!--well thats easy to fix i think XD eu vou diminuir as letras e tb tratar do  padding-->
<!--acho q se pode tirar o texto antes da navbar e centra-la-->
<!--tb eu tb so meti aquilo so para ver como ficava -->
<!--brb-->
<!--oki-->
<!--- i managed to get out somehow smh
    but now my cursor is broken-->
<!--want to restart?-->
<!--i have to, idk where i am messing up with the fooken code-->
<!--me is gonna start server-->
<!--");
            WriteLiteral(@"okiii-->
<!--
   arroba Html.ActionLink(""Edit"", ""Edit"", new { /* id=item.PrimaryKey */ })
   arroba Html.ActionLink(""Delete"", ""Delete"", new { /* id=item.PrimaryKey */ })
-->
<!--como é que queres que a parte do botão de detalhes seja feita? also brb-->
<!--sry taking so long, was creating a trigger on the database
    i was thinking about a button-->
<!--yeah but how is it going to work, tipo se abre uma janela, ou pop up or what? -->
<!--se calhar podemos ir p o modal-->
<!--e da para meter tipo uma imagem ou so queres texto a descrever a atividade?-->
<!--p a atividade se calhar pomos um texto
wdyt?-->
<!--yeah mas a cena para meter um texto temos de arrajnar uma forma pq nao pode ficar dentro do foreach  -->
<!--ent n sei se pomos num modal ou n, pq p mim acho q fiaria mais facil por um so campo invisivel c o id da ativ e d passar esse id p a outra pag
    mas se tiveres uma ideia d como fzr p ficar o modal ent conta ai, pq n tou a ver agr-->
<!--a cena e que eu tb nao tou a ver mais ideias");
            WriteLiteral(@" a nao ser criamos mais paginas e uma fica para cada atividade-->
<!--so precisamos d uma pag, dps no controllador passamos a informacao p a view, n precisamos d criar crls d pags-->
<!--oh oki entao queres fazer dessa forma?-->
<!--pelo menos p mim ficava menos confuso-->
<!--oki then lets do it
    diz me o codigo que eu tenho de fazer tho-->
<!--n tou a ver mt bem como vamos passar o id p o controller, o q e q sugeres?-->
<!--talvez se for possivel ao clicar no botao guarda o id e leva para o controller?-->
<!--e se fizessemos um form so c um input hidden c o id e o botao dos detalhes? faziamos um div p cada form desses
    deixa ver os tutoriais q ainda n percebi mt bem como passar ids lmao-->
<!--como assim?-->
<!--oki, tas a ver os do indiano? XD-->
<!--n, ate agr so vi os vids quase tds do tuga-->
<!--tipo o gajo explicava bem so que ele e tao chato XD-->
<!--lmao tava a por os fones p ouvir o video, mas n tava a dar som
    ainda n tinha posto os fones-->
<!--oh i hate when that happen");
            WriteLiteral(@"s to me XD-->
<!--a cena e q eu tnh 2 perfis d audio
    qd tnh o audio a sair p os altifalantes, o audio ta mutado
    so qd tnh fones no jack e q sai audio-->
<!--damm so basically u don't know if its the com ou se tens fones-->
<!--olha btw volto aqui a tarde , see yah then -->
<!--how about aqui no foreach em vez ser por td é por <p> ou metemos toda a informação dentro de um
<p> ou deixamos cada um em <p> diferentes no entanto metemos o html.dislay name for em cima de cada tópico
        mas so metemos o nome,data de inicio e final e tb tiramos algumas cenas que nao precisam de aparecer -->
<!--pds meter nome e datas, dps aparece um link a dzr ""detalhes"" e ai aparece tudo o q ta na bd (exceto probably o id)-->
<!--oki will do-->
<!--se calhar pomos o id num hidden input p dps ser mais facil aceder aos detalhes qd for fzr a logica-->
<!--yea we could do that -->
<!--well in that case vou ter d mudar a logica no controller lmao
        consegues mudar a pagina d modo a q as atividades aparecam");
            WriteLiteral(@" dentro d cartoes em vez d numa tabela?-->
<!--que tal por agora deixarmos dentro das tabelas e dp se virmos que fica mal alteramos?-->
<!--fica mal dentro d tabelas imo
    pelo menos isto e os eventos-->
<!--oki then give me a few minutes to see what i can do-->
<!--tenta n fzr isto dentro do corpo d uma tabela-->
<!--I know im still removing the table elements-->
<!--podemos utilizar isto para fazer a parte dos detalhes e tb as funções que estão aqui, ou achas que devemos
   tirar um ou dois?-->
<!--so deixamos editar e eliminar se o user for o adminagr
o resto pd td ver detalhes-->
<!--okiii doquiiiiiiiiiiiiii-->
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ScoutGestWeb.Models.AtividadeViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
