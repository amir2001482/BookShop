#pragma checksum "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3191f0636d77ebf66fdff036b97b05b78766dc5e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_City_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/City/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/City/Index.cshtml", typeof(AspNetCore.Areas_Admin_Views_City_Index))]
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
#line 1 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\_ViewImports.cshtml"
using BookShop;

#line default
#line hidden
#line 2 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\_ViewImports.cshtml"
using BookShop.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3191f0636d77ebf66fdff036b97b05b78766dc5e", @"/Areas/Admin/Views/City/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ee4735df80bb67d1ce7d40fc94d37240032cc50", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_City_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BookShop.Models.Provice>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Admin.cshtml";

#line default
#line hidden
            BeginContext(173, 180, true);
            WriteLiteral("<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div class=\"card\">\r\n            <div class=\"bg-light card-header\">\r\n                <div>\r\n                    ???????????? ?????????? ");
            EndContext();
            BeginContext(354, 18, false);
#line 12 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
                            Write(Model.ProvinceName);

#line default
#line hidden
            EndContext();
            BeginContext(372, 319, true);
            WriteLiteral(@"
                </div>
                <div>
                    <a href=""/Admin/Province/Index"" class=""float-left""><i class=""fa fa-arrow-circle-left""></i>  ???????????? ???? ???????? ?????????? ????   </a>
                </div>

            </div>
            <div class=""card-body"">
                <p>
                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 691, "\"", 734, 2);
            WriteAttributeValue("", 698, "/Admin/City/Create/", 698, 19, true);
#line 21 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
WriteAttributeValue("", 717, Model.ProvinceID, 717, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(735, 440, true);
            WriteLiteral(@" class=""btn btn-primary"">???????????? ?????? ????????</a>
                </p>
                <table class=""table table-bordered table-striped"">
                    <thead>
                        <tr>
                            <th>?????? ??????</th>
                            <th>
                                ????????????
                            </th>
                        </tr>
                    </thead>
                    <tbody>
");
            EndContext();
#line 33 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
                         foreach (var item in Model.City)
                        {

#line default
#line hidden
            BeginContext(1261, 70, true);
            WriteLiteral("                            <tr>\r\n                                <td>");
            EndContext();
            BeginContext(1332, 13, false);
#line 36 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
                               Write(item.CityName);

#line default
#line hidden
            EndContext();
            BeginContext(1345, 83, true);
            WriteLiteral("</td>\r\n                                <td>\r\n                                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1428, "\"", 1464, 2);
            WriteAttributeValue("", 1435, "/Admin/City/Edit/", 1435, 17, true);
#line 38 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
WriteAttributeValue("", 1452, item.CityID, 1452, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1465, 115, true);
            WriteLiteral(" class=\"btn btn-success btn-icon\"><i class=\"fa fa-edit text-white\"></i></a>\r\n                                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1580, "\"", 1618, 2);
            WriteAttributeValue("", 1587, "/Admin/City/Delete/", 1587, 19, true);
#line 39 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
WriteAttributeValue("", 1606, item.CityID, 1606, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1619, 151, true);
            WriteLiteral(" class=\"btn btn-danger btn-icon\"><i class=\"fa fa-trash text-white\"></i></a>\r\n                                </td>\r\n                            </tr>\r\n");
            EndContext();
#line 42 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\City\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(1797, 112, true);
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BookShop.Models.Provice> Html { get; private set; }
    }
}
#pragma warning restore 1591
