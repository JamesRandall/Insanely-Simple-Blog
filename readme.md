# Insanely Simple Blog

Insanely Simple Blog is just that - an insanely simple blog. It has a Backbone/JavaScript based UI powered by a set of Web Api services. It's designed to be easy to drop into a single page on an existing MVC4 web site to add basic blog functionality - because the UI is based on Backbone and JavaScript no pages need adding to the site other than the host page.

The look and feel of the blog can be controlled through CSS styles that are all isolated within a .insanelysimpleblog root class selector. For additional configuration the Handlebar templates can be modified.

Posts are written in the Markdown markup language and uploaded to the blog via a PowerShell cmdlet. Similarly deploying the required database is done through PowerShell.

Normal URL linking to posts etc. is supported via the Backbone App Router.

*Please note that the code as it stands now requires a little more work and the intention is to release this as a NuGet package in the next few ways so that adding the Package Reference does 90% of the work. Installation instructions will accompany that package.*

## Why?

Why write another blog system? I didn't really mean to. I just set out to tinker with the new Web Api and MVC4 features and ended up with something half useful and decided to finish it off.

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