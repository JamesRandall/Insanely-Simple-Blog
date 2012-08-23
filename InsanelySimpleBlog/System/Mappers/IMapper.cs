namespace InsanelySimpleBlog.System.Mappers
{
    internal interface IMapper<in T1, out T2>
    {
        T2 Map(T1 @from);
    }
}
