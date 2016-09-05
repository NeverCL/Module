using System.Collections.Generic;
using System.Linq;

namespace Module.Application
{
    public static class PageExtensions
    {
        public static PageResult<TDestination> ToPage<TDestination, TSource>(this IQueryable<TSource> data, PageRequest pageRequest)
            where TDestination : class
        {
            var items = data.Skip(pageRequest.Size * (pageRequest.Index - 1)).Take(pageRequest.Size);
            return new PageResult<TDestination>
            {
                Count = data.Count(),
                Data = items.MapTo<List<TDestination>>(),
                Index = pageRequest.Index,
                Size = pageRequest.Size
            };
        }
    }
}
