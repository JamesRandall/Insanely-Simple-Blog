using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace InsanelySimpleBlog.PowerShell
{
    public abstract class AbstractDatabaseCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "The database server to add the blog post to. Defaults to (local)")]
        public string Server { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The database to add the blog post to. Defaults to InsanelySimpleBlog")]
        public string Database { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The username to connect to the database as. If unspecified then integrated security will be used.")]
        public string Username { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "If a username is specified then the password should also be specified")]
        public string Password { get; set; }

        protected AbstractDatabaseCmdlet()
        {
            Server = "(local)";
            Database = "InsanelySimpleBlog";
            Username = null;
            Password = null;
        }

        protected string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = Server;
            builder.InitialCatalog = Database;

            if (String.IsNullOrWhiteSpace(Username))
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = Username;
                builder.Password = Password;
            }

            return builder.ConnectionString;
        }
    }
}
