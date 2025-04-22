namespace H.Tools.EntityFramework;

public static class EFExtend
{
    public static IQueryable<TEntity> When<TEntity>(this IQueryable<TEntity> source, Func<bool> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> action) where TEntity : class
    {
        if (predicate())
        {
            return action(source);
        }
        else
        {
            return source;
        }
    }
}
