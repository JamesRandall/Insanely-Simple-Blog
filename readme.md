# Insanely Simple Blog

## Introduction

Insanely Simple Blog is just that - an insanely simple blog. It has a Backbone/JavaScript based UI powered by a set of Web Api services. It's designed to be easy to drop into a single page on an existing MVC4 web site to add basic blog functionality - because the UI is based on Backbone and JavaScript no pages need adding to the site other than the host page.

The look and feel of the blog can be controlled through CSS styles that are all isolated within a .insanelysimpleblog root class selector. For additional configuration the Handlebar templates can be modified.

Posts are written in the Markdown markup language and uploaded to the blog via a PowerShell cmdlet. Similarly deploying the required database is done through PowerShell.

Normal URL linking to posts etc. is supported via the Backbone App Router.

## Why?

Why write another blog system? I didn't really mean to. I just set out to tinker with the new Web Api and MVC4 features and ended up with something half useful and decided to finish it off.

## Installation

The easiest way to install the blog is through NuGet. Either find the InsanelySimpleBlog package in the GUI or in the console type:

    Install-Package InsanelySimplyBlog

The blog will slot nicely into a default MVC4 project structure but if you tinker with things obviously you may need to do work to fit it in.

## Usage

After installation you will need to perform a number of steps to complete configuration and post, errrr, posts.

### Configure a Database

In the Package Manager Console run the command:

    New-Deployment -Author "Your Name" -BlogName "Your Blog Name" -BlogPageUrl "http://localhost/blog"

By default the blog will be installed into a local default instance SQL Server with a database name of InsanelySimpleBlog.
You can change this using the -Server and -Database options. If you are not using Windows Security you can also supply a
username and password using the -Username and -Password options.

Note that as part of the installation your web.config will have had a connection string added that also points to the
default server and database described above.

### Style the blog

This step is optional but to apply the default style to the blog add a link to the included stylesheet in your
_layout.cshtml page in the <head> section. For example:

    @Styles.Render("~/Content/insanelySimpleBlogCss")

### Upload Posts

Upload a post using the Add-Post command in the Package Manager Console. Insanely Simple Blog uses the markdown
format and the first line of the markdown file is assumed to be the subject. For example:

    Add-Post -Filename mypost.md

You can optionally assign a post to one or more categories using the -Categories option that takes a comma delimited
list of categories:

    Add-Post -Filename mysecondpost.md -Categories="Category One,Category Two"

By default the post will be given the current date but you can specify a different posted date using the PostedAt
option, for example the below will set the post to be posted at January 28th 2012 at 10:00 AM:

    Add-Post -Filename mythirdpost.md -PostedAt 2012-01-28T10:00:00

Add-Post can also accept the same set as database options as New-Deployment (-Server, -Database, -Username and
-Password).

### View the Blog

Your blog will be located at http://localhost:{port}/blog via the created BlogController and Index.cshtml. They can easily be moved elsewhere if required.

### Accessing the PowerShell Commands

The PowerShell commands referenced above are available in a normal PowerShell console as well as in the Package Manager Console of any solution that uses the NuGet package. You will require the 
assemblies located in the tools folder of the downloaded package. You will need to import the module:

Import-Module -Name InsanelySimpleBlog.PowerShell.dll

## Building the NuGet Package

If you change the code and want to build yourself a NuGet package you can use the PowerShell script located in the NuGetBuild folder. In the PowerShell console move into the NuGetBuild folder and execute the script:

    .\buildNuGetPackage.ps1

## Feedback and Help

You can send feedback to me at

> [support@accidentalfish.com](support@accidentalfish.com)

Or via my blog at

> [http://accidentalfish.wordpress.com](http://accidentalfish.wordpress.com)

If I can help I will!

## Thanks

As usual this project stands upon the shoulders of open source giants, in this case:

- jQuery
- Backbone
- Handlebars
- Markdown
- NuGet

Thanks to the many contributors to those projects. You guys rock.

## License

The code is distributed under the MIT License.

> Copyright (C) 2012 Accidental Fish Ltd.
>
 Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
>
 The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
>
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.