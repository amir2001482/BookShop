#pragma checksum "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c279"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_UserManager_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/UserManager/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/UserManager/Index.cshtml", typeof(AspNetCore.Areas_Admin_Views_UserManager_Index))]
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
#line 2 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
using ReflectionIT.Mvc.Paging;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73c49b30dcfbf10f9c90a34ee8b0d44f4f27c279", @"/Areas/Admin/Views/UserManager/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ee4735df80bb67d1ce7d40fc94d37240032cc50", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_UserManager_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ReflectionIT.Mvc.Paging.PagingList<BookShop.Models.ViewModels.UsersManagerViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/UserPic.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("40"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger btn-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SendEmail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Admin.cshtml";

#line default
#line hidden
            BeginContext(252, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 9 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
 if (ViewBag.Alert != null)
{

#line default
#line hidden
            BeginContext(286, 65, true);
            WriteLiteral("    <div class=\"alert alert-success alert-dismissable\">\r\n        ");
            EndContext();
            BeginContext(352, 13, false);
#line 12 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
   Write(ViewBag.Alert);

#line default
#line hidden
            EndContext();
            BeginContext(365, 172, true);
            WriteLiteral("\r\n        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n            <span aria-hidden=\"true\">&times;</span>\r\n        </button>\r\n    </div>\r\n");
            EndContext();
#line 17 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
}

#line default
#line hidden
            BeginContext(540, 603, true);
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-12"">
        <div class=""card"">
            <div class=""card-header bg-light"">
                ?????????????? ????????
            </div>
            <div class=""card-body"">
                <p>
                    <a href=""/Identity/Account/Register"" class=""btn btn-primary"">???????????? ?????????? ????????</a>
                    <a class=""btn btn-success"" data-toggle=""collapse"" href=""#collapseExample"" role=""button"" aria-expanded=""false"" aria-controls=""collapseExample"">
                        ?????????? ??????????
                    </a>
                </p>
                ");
            EndContext();
            BeginContext(1143, 7421, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c2799000", async() => {
                BeginContext(1172, 1412, true);
                WriteLiteral(@"
                    <div class=""collapse"" id=""collapseExample"">
                        <div class=""card card-body"">
                            <div class=""custom-control form-group custom-checkbox d-inline-block"">
                                <input type=""checkbox"" class=""custom-control-input"" id=""All"" />
                                <label class=""custom-control-label"" for=""All"">?????????? ?????????? ???????? ?????? ??????????????</label>
                            </div>
                            <input type=""text"" class=""form-control form-group"" name=""subject"" placeholder=""?????????? ??????????"" />
                            <textarea class=""form-control form-group summernote"" name=""message"">
                            ?????????? ?????? ???? ??????????????...
                            </textarea>
                            <input type=""submit"" value=""?????????? ??????????"" class=""btn btn-primary float-left"" />
                        </div>
                    </div>
                    <div class=""table-responsive"">
                  ");
                WriteLiteral(@"      <table class=""table table-striped table-bordered"">
                            <thead>
                                <tr>
                                    <th class=""text-center"">
                                        ????????????
                                    </th>
                                    <th class=""text-center"">
                                        ");
                EndContext();
                BeginContext(2585, 44, false);
#line 54 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.Image));

#line default
#line hidden
                EndContext();
                BeginContext(2629, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(2777, 48, false);
#line 57 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.FirstName));

#line default
#line hidden
                EndContext();
                BeginContext(2825, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(2973, 47, false);
#line 60 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.LastName));

#line default
#line hidden
                EndContext();
                BeginContext(3020, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(3168, 47, false);
#line 63 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.UserName));

#line default
#line hidden
                EndContext();
                BeginContext(3215, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(3363, 50, false);
#line 66 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.PhoneNumber));

#line default
#line hidden
                EndContext();
                BeginContext(3413, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(3561, 44, false);
#line 69 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.Email));

#line default
#line hidden
                EndContext();
                BeginContext(3605, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(3753, 44, false);
#line 72 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.Roles));

#line default
#line hidden
                EndContext();
                BeginContext(3797, 147, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">\r\n                                        ");
                EndContext();
                BeginContext(3945, 47, false);
#line 75 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                   Write(Html.SortableHeaderFor(model => model.IsActive));

#line default
#line hidden
                EndContext();
                BeginContext(3992, 232, true);
                WriteLiteral("\r\n                                    </th>\r\n                                    <th class=\"text-center\">????????????</th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n");
                EndContext();
#line 81 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
                BeginContext(4321, 318, true);
                WriteLiteral(@"                                    <tr>
                                        <td class=""text-center"">
                                            <div class=""custom-control custom-checkbox d-inline-block"">
                                                <input type=""checkbox"" class=""custom-control-input child""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 4639, "\"", 4658, 1);
#line 86 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
WriteAttributeValue("", 4647, item.Email, 4647, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(4659, 14, true);
                WriteLiteral(" name=\"emails\"");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 4673, "\"", 4686, 1);
#line 86 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
WriteAttributeValue("", 4678, item.Id, 4678, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(4687, 88, true);
                WriteLiteral(" />\r\n                                                <label class=\"custom-control-label\"");
                EndContext();
                BeginWriteAttribute("for", " for=\"", 4775, "\"", 4789, 1);
#line 87 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
WriteAttributeValue("", 4781, item.Id, 4781, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(4790, 176, true);
                WriteLiteral("></label>\r\n                                            </div>\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n");
                EndContext();
#line 91 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                             if (item.Image != null)
                                            {

                                            }

                                            else
                                            {

#line default
#line hidden
                BeginContext(5231, 48, true);
                WriteLiteral("                                                ");
                EndContext();
                BeginContext(5279, 46, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c27918317", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(5325, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 99 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                            }

#line default
#line hidden
                BeginContext(5374, 157, true);
                WriteLiteral("                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(5532, 44, false);
#line 102 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
                EndContext();
                BeginContext(5576, 159, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(5736, 43, false);
#line 105 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
                EndContext();
                BeginContext(5779, 159, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(5939, 43, false);
#line 108 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
                EndContext();
                BeginContext(5982, 159, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(6142, 46, false);
#line 111 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.PhoneNumber));

#line default
#line hidden
                EndContext();
                BeginContext(6188, 159, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(6348, 40, false);
#line 114 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
                EndContext();
                BeginContext(6388, 115, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n");
                EndContext();
#line 117 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                             foreach (var role in item.Roles)
                                            {

#line default
#line hidden
                BeginContext(6629, 136, true);
                WriteLiteral("                                                <span class=\"badge badge-primary\">\r\n                                                    ");
                EndContext();
                BeginContext(6766, 4, false);
#line 120 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                               Write(role);

#line default
#line hidden
                EndContext();
                BeginContext(6770, 59, true);
                WriteLiteral("\r\n                                                </span>\r\n");
                EndContext();
#line 122 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                            }

#line default
#line hidden
                BeginContext(6876, 164, true);
                WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            <span");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 7040, "\"", 7107, 2);
                WriteAttributeValue("", 7048, "badge", 7048, 5, true);
#line 126 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
WriteAttributeValue(" ", 7053, item.IsActive==true?"badge-success":"badge-danger", 7054, 53, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(7108, 51, true);
                WriteLiteral(">\r\n                                                ");
                EndContext();
                BeginContext(7161, 42, false);
#line 127 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                            Write(item.IsActive == true ? "????????" : "??????????????");

#line default
#line hidden
                EndContext();
                BeginContext(7204, 212, true);
                WriteLiteral("\r\n                                            </span>\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
                EndContext();
                BeginContext(7416, 222, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c27925649", async() => {
                    BeginContext(7497, 137, true);
                    WriteLiteral("\r\n                                                <i class=\"fa fa-user\"></i> | ???????????? ??????????\r\n                                            ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 131 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                                                      WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(7638, 48, true);
                WriteLiteral("\r\n\r\n                                            ");
                EndContext();
                BeginContext(7686, 204, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c27928415", async() => {
                    BeginContext(7764, 122, true);
                    WriteLiteral("\r\n                                                <i class=\"fa fa-edit\"></i>\r\n                                            ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 135 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                                                   WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(7890, 46, true);
                WriteLiteral("\r\n                                            ");
                EndContext();
                BeginContext(7936, 206, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "73c49b30dcfbf10f9c90a34ee8b0d44f4f27c27931159", async() => {
                    BeginContext(8015, 123, true);
                    WriteLiteral("\r\n                                                <i class=\"fa fa-trash\"></i>\r\n                                            ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 138 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                                                     WriteLiteral(item.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(8142, 96, true);
                WriteLiteral("\r\n\r\n\r\n                                        </td>\r\n                                    </tr>\r\n");
                EndContext();
#line 145 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                                }

#line default
#line hidden
                BeginContext(8273, 131, true);
                WriteLiteral("                            </tbody>\r\n                        </table>\r\n                        <nav>\r\n                            ");
                EndContext();
                BeginContext(8405, 74, false);
#line 149 "D:\Poragraming\ASP.NetCore\BookShopp\BookShop\Areas\Admin\Views\UserManager\Index.cshtml"
                       Write(await this.Component.InvokeAsync("Pager", new { PagingList = this.Model }));

#line default
#line hidden
                EndContext();
                BeginContext(8479, 78, true);
                WriteLiteral("\r\n                        </nav>\r\n                    </div>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(8564, 963, true);
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {
        $('.summernote').summernote({
            toolbar: [
                // [groupName, [list of button]]
                ['style', ['bold', 'italic', 'underline', 'clear', 'strikethrough', 'style']],
                ['fontname', ['fontname']],
                ['fontsize', ['fontsize']],
                ['forecolor', ['forecolor']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['insert', ['link', 'picture', 'hr']],
                ['view', ['fullscreen', 'codeview']]
            ],

            height: 300,
            lang: 'fa-IR'
        });
    });

    $(function () {
        $(""#All"").on('change', function () {
            $("".child"").prop('checked', $(this).prop('checked'));
        });
    });

</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ReflectionIT.Mvc.Paging.PagingList<BookShop.Models.ViewModels.UsersManagerViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
