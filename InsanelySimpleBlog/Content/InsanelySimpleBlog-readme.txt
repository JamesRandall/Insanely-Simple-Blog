To complete setup of the blog perform the following three steps to complete configuration and upload blog posts:

1) In the Package Manager Console run the command:

    New-Deployment -Author "Your Name" -BlogName "Your Blog Name" -BlogPageUrl "http://localhost/blog"

By default the blog will be installed into a local default instance SQL Server with a database name of InsanelySimpleBlog.
You can change this using the -Server and -Database options. If you are not using Windows Security you can also supply a
username and password using the -Username and -Password options.

Note that as part of the installation your web.config will have had a connection string added that also points to the
default server and database described above.

2) This step is optional but to apply the default style to the blog add a link to the included stylesheet in your
_layout.cshtml page in the <head> section. For example:

    @Styles.Render("~/Content/insanelySimpleBlogCss")

3) Upload a post using the Add-Post command in the Package Manager Console. Insanely Simple Blog uses the markdown
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

4) Your blog will be located at http://localhost:{port}/blog via the created BlogController and Index.cshtml


The PowerShell commands referenced above are available in a normal PowerShell console. You will require the 
assemblies located in the tools folder of the downloaded package. You will need to import the module:

Import-Module -Name InsanelySimpleBlog.PowerShell.dll

It's also worth noting that this NuGet package is "Sql Azure" friendly and includes error handling for SQL Azure
transient errors.

You can send feedback to me at support@accidentalfish.com or via my blog at http://accidentalfish.wordpress.com

The source code is available on GitHub at https://github.com/JamesRandall/Insanely-Simple-Blog