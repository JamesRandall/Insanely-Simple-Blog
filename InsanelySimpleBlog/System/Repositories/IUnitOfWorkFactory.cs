namespace InsanelySimpleBlog.System.Repositories
{
    internal interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
