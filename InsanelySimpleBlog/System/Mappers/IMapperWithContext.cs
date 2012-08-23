namespace InsanelySimpleBlog.System.Mappers
{
    public interface IMapperWithContext<in T1, out T2, in T3>
    {
        T2 Map(T1 @from, T3 context);
    }
}
