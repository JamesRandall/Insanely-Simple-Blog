using System.Configuration;

namespace InsanelySimpleBlog.System.Configuration.Implementation
{
    internal class Configuration : IConfiguration
    {
        public string SqlConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["InsanelySimpleBlogDb"].ConnectionString; }
        }
    }
}
