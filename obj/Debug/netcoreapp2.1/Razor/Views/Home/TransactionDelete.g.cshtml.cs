#pragma checksum "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec1895aeef2a85c01cffbc2d03d0ac9d1c15015f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_TransactionDelete), @"mvc.1.0.view", @"/Views/Home/TransactionDelete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/TransactionDelete.cshtml", typeof(AspNetCore.Views_Home_TransactionDelete))]
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
#line 1 "/home/mark/Projects/farmdbvsc/Views/_ViewImports.cshtml"
using farmdbvsc;

#line default
#line hidden
#line 2 "/home/mark/Projects/farmdbvsc/Views/_ViewImports.cshtml"
using FarmDBMongoV1.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec1895aeef2a85c01cffbc2d03d0ac9d1c15015f", @"/Views/Home/TransactionDelete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4c76d8df53d5a5bcf150c18e989bb5dcfb23999", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_TransactionDelete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FarmDBMongoV1.Models.Transaction>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(41, 155, true);
            WriteLiteral("\r\n<h2>Transaction Delete</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n\t<h4>Transaction</h4>\r\n\t<hr />\r\n\t<dl class=\"dl-horizontal\">\r\n\t\t<dt>");
            EndContext();
            BeginContext(197, 51, false);
#line 10 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.TransactionDate));

#line default
#line hidden
            EndContext();
            BeginContext(248, 13, true);
            WriteLiteral("</dt>\r\n\t\t<dd>");
            EndContext();
            BeginContext(262, 47, false);
#line 11 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayFor(model => model.TransactionDate));

#line default
#line hidden
            EndContext();
            BeginContext(309, 13, true);
            WriteLiteral("</dd>\r\n\t\t<dt>");
            EndContext();
            BeginContext(323, 58, false);
#line 12 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.TransactionDescription));

#line default
#line hidden
            EndContext();
            BeginContext(381, 13, true);
            WriteLiteral("</dt>\r\n\t\t<dd>");
            EndContext();
            BeginContext(395, 54, false);
#line 13 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayFor(model => model.TransactionDescription));

#line default
#line hidden
            EndContext();
            BeginContext(449, 13, true);
            WriteLiteral("</dd>\r\n\t\t<dt>");
            EndContext();
            BeginContext(463, 50, false);
#line 14 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.Account1Amount));

#line default
#line hidden
            EndContext();
            BeginContext(513, 13, true);
            WriteLiteral("</dt>\r\n\t\t<dd>");
            EndContext();
            BeginContext(527, 46, false);
#line 15 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
       Write(Html.DisplayFor(model => model.Account1Amount));

#line default
#line hidden
            EndContext();
            BeginContext(573, 15, true);
            WriteLiteral("</dd>\r\n\t</dl>\r\n");
            EndContext();
#line 17 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
     using (Html.BeginForm())
	{
		

#line default
#line hidden
            BeginContext(623, 23, false);
#line 19 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(657, 49, false);
#line 20 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
      Write(Html.Hidden("personid", (string)ViewBag.personid));

#line default
#line hidden
            EndContext();
#line 20 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
                                                             ;
        

#line default
#line hidden
            BeginContext(718, 49, false);
#line 21 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
   Write(Html.Hidden("firstday", (string)ViewBag.firstday));

#line default
#line hidden
            EndContext();
#line 21 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
                                                          ;
        

#line default
#line hidden
            BeginContext(779, 47, false);
#line 22 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
   Write(Html.Hidden("lastday", (string)ViewBag.lastday));

#line default
#line hidden
            EndContext();
#line 22 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
                                                        ;
		

#line default
#line hidden
            BeginContext(832, 33, false);
#line 23 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
   Write(Html.HiddenFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(867, 108, true);
            WriteLiteral("\t\t<div class=\"form-actions no-color\">\r\n\t\t<input type=\"submit\" value=\"Delete\" class=\"btn btn-default\" /> \r\n\t\t");
            EndContext();
            BeginContext(976, 149, false);
#line 26 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
   Write(Html.ActionLink("Back to List", "TransactionList",  "Home", new {
		personid=ViewBag.personid, firstday=ViewBag.firstday, lastday=ViewBag.lastday }));

#line default
#line hidden
            EndContext();
            BeginContext(1125, 12, true);
            WriteLiteral("\r\n\t\t</div>\r\n");
            EndContext();
#line 29 "/home/mark/Projects/farmdbvsc/Views/Home/TransactionDelete.cshtml"
	}

#line default
#line hidden
            BeginContext(1141, 10, true);
            WriteLiteral("</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FarmDBMongoV1.Models.Transaction> Html { get; private set; }
    }
}
#pragma warning restore 1591
