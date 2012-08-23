namespace InsanelySimpleBlog.System.Policies
{
    internal static class RetryPolicyType
    {
        public const string Sql = "Sql";
        public const string NoSql = "NoSql";
        public const string Queue = "Queue";
    }
}
