#pragma checksum "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e10072bf0f8939cb56eb1628ffd4d9364ba2b8d5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Interest_Overview), @"mvc.1.0.view", @"/Views/Interest/Overview.cshtml")]
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
#line 1 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\_ViewImports.cshtml"
using Managment.Portal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\_ViewImports.cshtml"
using Managment.Portal.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\_ViewImports.cshtml"
using Managment.Portal.ViewComponents;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e10072bf0f8939cb56eb1628ffd4d9364ba2b8d5", @"/Views/Interest/Overview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d3fd04402a6f7748b43662371bd8d3543b5128f", @"/Views/_ViewImports.cshtml")]
    public class Views_Interest_Overview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<InterestOverviewViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<h1 class=""text-center alert-info"">
    Overview
</h1>
<div>


    <table class=""table table-striped"">
        <th></th>
        <th>Animal</th>
        <th>Customer</th>
        <th>Telephone Number</th>
        <th>Comment</th>
        <th>Added On</th>
        </thead>
        <tbody>

");
#nullable restore
#line 19 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
             foreach (var interest in @Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n");
#nullable restore
#line 23 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                           if (interest.Animal.Image != null)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <img");
            BeginWriteAttribute("src", " src=\"", 588, "\"", 658, 2);
            WriteAttributeValue("", 594, "data:image;base64,", 594, 18, true);
#nullable restore
#line 25 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
WriteAttributeValue("", 612, Convert.ToBase64String(interest.Animal.Image), 612, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"avatar rounded-circle\" alt=\"picture animal\" />\r\n");
#nullable restore
#line 26 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <img class=\"avatar rounded-circle\" src=\"http://i.imgur.com/e9ABPu5.jpg\" alt=\"default picture\" />\r\n");
#nullable restore
#line 30 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                    <td>");
#nullable restore
#line 33 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                    Write($"{interest.Animal.Name}");

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 34 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                    Write($"{interest.Customer.FirstName} {interest.Customer.LastName}");

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 35 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                    Write(interest.Customer.TelephoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                    Write(interest.Comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 37 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
                    Write($"{interest.AddedOn:dd-MM-yyyy}");

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 39 "C:\Users\ys200\Desktop\learn\Serverside\Animal Shelter\Managment.Portal\Views\Interest\Overview.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tbody>\r\n\r\n    </table>\r\n\r\n    <style>\r\n        .avatar {\r\n            vertical-align: middle;\r\n            width: 50px;\r\n            height: 50px;\r\n            border-radius: 50%;\r\n        }\r\n    </style>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<InterestOverviewViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
