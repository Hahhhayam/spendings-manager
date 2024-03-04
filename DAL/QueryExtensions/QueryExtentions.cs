using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.QueryExtensions
{
    public static class QueryExtensions
    {
        public static T? GetById<T>(this IQueryable<T> query, int? id) where T : BaseEntity
        {
            return query
               .AsNoTracking()
               .SingleOrDefault(e => e.Id == id);
        }
    }
}
