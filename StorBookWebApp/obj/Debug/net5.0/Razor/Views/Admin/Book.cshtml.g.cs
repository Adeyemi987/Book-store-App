#pragma checksum "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05a3518fbd3cf82acae81a72e70163006c57c82f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Book), @"mvc.1.0.view", @"/Views/Admin/Book.cshtml")]
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
#line 1 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using StorBookWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using StorBookWebApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using StorBookWebApp.Core.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using StorBookWebApp.Controllers.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\_ViewImports.cshtml"
using System.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"05a3518fbd3cf82acae81a72e70163006c57c82f", @"/Views/Admin/Book.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4de92141cd742e7b1ca239e9720a31b88d7ec6b3", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Book : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<StorBookWebApp.Core.MVC.BookDTo>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditBook", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteBook", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
  
    ViewData["Title"] = "Admin Book Page";
    Layout = "_AdminPage";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div");
            BeginWriteAttribute("class", " class=\"", 140, "\"", 148, 0);
            EndWriteAttribute();
            WriteLiteral(@">
    <div class=""projects"">
        <div class=""card"">
            <div class=""card-header"">
                <a>Add New Book</a>
            </div>

            <!-- <div class=""card-header"">
                <h3>Books</h3>

                <button>See All <span class=""las la-arrow-right"">
                </span></button>
            </div> -->


            <div class=""card-body"">
                <div class=""table-responsive"">
                    <table width=""100%"">
                        <thead>
                            <tr>
                                <td></td>
                                <td>Book Title</td>
                                <td>Author Name</td>
                                <td>Date Published</td>
                                <td>Total Ratings</td>
                                <td>Date Uploaded</td>

                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 38 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n");
            WriteLiteral("                                    <td></td>\r\n                                    <td>\r\n");
#nullable restore
#line 44 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                          
                                        var item1 = "";
                                            if (@item.Title.Length > 17)
                                            {
                                               item1 = @item.Title.Substring(0, 17) + " ...";
                                            }
                                            else{
                                                item1 = @item.Title;
                                            }
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
#nullable restore
#line 54 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                   Write(item1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n");
#nullable restore
#line 57 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                          
                                        var item2 = "";
                                            if (@item.AuthorsOrdered.Contains(","))
                                            {
                                               item2 = @item.AuthorsOrdered.Substring(0, @item.AuthorsOrdered.IndexOf(","));
                                            }
                                            else{
                                                item2 = @item.AuthorsOrdered;
                                            }
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
#nullable restore
#line 67 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                   Write(item2);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 70 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                   Write(item.PublishedOn);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>");
#nullable restore
#line 72 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                   Write(item.ReviewsCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 73 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                   Write(item.Created.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td style=\"margin-top:13px\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "05a3518fbd3cf82acae81a72e70163006c57c82f10396", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 75 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                                                                          WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("|\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 3406, "\"", 3413, 0);
            EndWriteAttribute();
            WriteLiteral(">Details</a>|\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "05a3518fbd3cf82acae81a72e70163006c57c82f13051", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                                                                                            WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </td>\r\n                            </tr>\r\n");
#nullable restore
#line 80 "C:\Users\hp\Desktop\week-10-team-e-week-10-project\StorBookWebApp\Views\Admin\Book.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<StorBookWebApp.Core.MVC.BookDTo>> Html { get; private set; }
    }
}
#pragma warning restore 1591
