using BookShop.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.TagHelpers
{
    [HtmlTargetElement("security-trimming")]
    public class SecurityTrimmingTagHelper : TagHelper
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;

        public SecurityTrimmingTagHelper(ISecurityTrimmingService securityTrimmingService)
        {
            _securityTrimmingService = securityTrimmingService;
        }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }


        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }


        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            if (!ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
            }

            string[] Actions = Action.Split(":");
            for(int i=0;i<Actions.Length;i++)
            {
                if (_securityTrimmingService.CanCurrentUserAccess(Area, Controller, Actions[i]))
                {
                    return;
                }
            }
           

            output.SuppressOutput();
        }
    }
}
