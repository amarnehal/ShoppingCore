#pragma checksum "D:\ShoppingCore\ShoppingCore\Views\Shared\Components\ShoppingCart\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "517d35240d9bc1f1e5083b225cca6a417f0a2ad6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_ShoppingCart_Default), @"mvc.1.0.view", @"/Views/Shared/Components/ShoppingCart/Default.cshtml")]
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
#line 1 "D:\ShoppingCore\ShoppingCore\Views\_ViewImports.cshtml"
using ShoppingCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ShoppingCore\ShoppingCore\Views\_ViewImports.cshtml"
using ShoppingCore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"517d35240d9bc1f1e5083b225cca6a417f0a2ad6", @"/Views/Shared/Components/ShoppingCart/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"536112eb7e754cf3972e0d14be0374e1ea555c1a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_ShoppingCart_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <i class=\"bi bi-bag\"></i>&nbsp;(");
#nullable restore
#line 2 "D:\ShoppingCore\ShoppingCore\Views\Shared\Components\ShoppingCart\Default.cshtml"
                               Write(Model);

#line default
#line hidden
#nullable disable
            WriteLiteral(")\r\n   \r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
