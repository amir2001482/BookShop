#pragma checksum "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "774328dc37c16f10800f994093e386e14a6dee09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\_ViewImports.cshtml"
using BookSope2;

#line default
#line hidden
#line 2 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\_ViewImports.cshtml"
using BookSope2.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"774328dc37c16f10800f994093e386e14a6dee09", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3c7718c6906ab10266ab1cc108535bac9ba9ad5d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 26, true);
            WriteLiteral("\r\n    <div class=\"mt-4\">\r\n");
            EndContext();
#line 6 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\Home\Index.cshtml"
         if (ViewBag.Msg != null)
        {

#line default
#line hidden
            BeginContext(117, 36, true);
            WriteLiteral("        <p class=\"alert alert-info\">");
            EndContext();
            BeginContext(154, 11, false);
#line 8 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\Home\Index.cshtml"
                               Write(ViewBag.Msg);

#line default
#line hidden
            EndContext();
            BeginContext(165, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 9 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Views\Home\Index.cshtml"
        }

#line default
#line hidden
            BeginContext(182, 12, true);
            WriteLiteral("    </div>\r\n");
            EndContext();
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
