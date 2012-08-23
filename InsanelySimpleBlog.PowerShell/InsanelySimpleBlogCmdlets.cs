using System.ComponentModel;
using System.Management.Automation;

namespace InsanelySimpleBlog.PowerShell
{
    [RunInstaller(true)]
    public class InsanelySimpleBlogCmdlets : PSSnapIn
    {
        public override string Name
        {
            get { return "InsanelySimpleBlog-Cmdlets"; }
        }

        public override string Vendor
        {
            get { return "Accidental Fish Ltd."; }
        }

        public override string Description
        {
            get
            {
                return "A set of Azure cmdlets for administering the Insanely Simple Blog by Accidental Fish Ltd.";
            }
        }
    }
}
