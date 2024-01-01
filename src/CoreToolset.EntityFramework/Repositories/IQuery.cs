namespace CoreToolset.EntityFramework.Repositories
{
    public interface IQuery<T>
    {
        IQueryable<T> Query();
    }
}
