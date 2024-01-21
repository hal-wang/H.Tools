using Microsoft.EntityFrameworkCore;

namespace H.Tools.EntityFramework;

public static class PageListExtend
{
    public static async Task<TOut> ToPageListAsync<TEntity, TOut>(this IQueryable<TEntity> query, PageListQuery dto) where TOut : PageListResult<TEntity>, new()
    {
        TEntity[] list;
        if (dto.All)
        {
            list = await query.ToArrayAsync();
        }
        else
        {
            list = await query.Skip(dto.Limit * (dto.Page - 1)).Take(dto.Limit).ToArrayAsync();
        }
        var total = await query.CountAsync();

        return new TOut()
        {
            Total = total,
            Limit = dto.Limit,
            List = list,
            Page = dto.Page,
        };
    }

    public static async Task<PageListResult<TEntity>> ToPageListAsync<TEntity>(this IQueryable<TEntity> query, PageListQuery dto)
    {
        return await query.ToPageListAsync<TEntity, PageListResult<TEntity>>(dto);
    }

    public static async Task<TOut> ToPageListAsync<TEntity, TOutType, TOut>(this IQueryable<TEntity> query, PageListQuery dto, Func<TEntity, TOutType> selector) where TOut : PageListResult<TOutType>, new()
    {
        TEntity[] list;
        if (dto.All)
        {
            list = await query.ToArrayAsync();
        }
        else
        {
            list = await query.Skip(dto.Limit * (dto.Page - 1)).Take(dto.Limit).ToArrayAsync();
        }
        var total = await query.CountAsync();

        return new TOut()
        {
            Total = total,
            List = list.Select(selector).ToArray(),
            Limit = dto.Limit,
            Page = dto.Page,
        };
    }

    public static async Task<PageListResult<TOutType>> ToPageListAsync<TEntity, TOutType>(this IQueryable<TEntity> query, PageListQuery dto, Func<TEntity, TOutType> selector)
    {
        return await query.ToPageListAsync<TEntity, TOutType, PageListResult<TOutType>>(dto, selector);
    }
}
