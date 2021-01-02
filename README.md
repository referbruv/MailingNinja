# RazorViewEngine Boilerplate

A sample application to demonstrate how to render model data into a View and extract the output HTML using the Razor capabilities in ASP.NET Core.

# What is RazorViewEngine?

RazorViewEngine is a view engine that converts the razor powered cshtml files in an MVC application into plain HTML documents which the browser can understand. It takes care of the variable resolution, loops and other control structures of the Razor syntax and processes them to form the HTML only which the browser can render and display to the user.

For starters, An MVC application developed over the ASP.NET framework internally uses Razor to render html views and data over layout files during a request. Applications developed using ASP.NET can make use of an external library called RazorEngine which levarages the Razor engine capabilities of the MVC framework. 

Although RazorEngine plugin has no direct support for ASP.NET Core, the aspnetcore framework still comes with the razor engine which provides helper methods which can by-produce the rendered HTML over the passed model data and return the plain HTML along.

The complete explanation of this example is available at:

https://referbruv.com/blog/posts/template-based-emails-fetch-rendered-view-html-in-aspnet-core-using-razor 

<a href="https://www.buymeacoffee.com/referbruv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>
