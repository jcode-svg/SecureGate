using Microsoft.EntityFrameworkCore;
using SecureGate.SharedKernel.Models;

namespace SecureGate.SharedKernel.HelperMethods
{
    public static class HelperMethods
    {
        public static async Task<(List<T> items, bool hasNextPage)> ApplyTo<T>(this IQueryable<T> query, PaginatedRequest request)
        {
            request.Page = request.Page == default ? 1 : request.Page;
            request.PageSize = request.PageSize == default ? 10 : request.PageSize;

            int skip = (request.Page - 1) * request.PageSize;
            var items = await query.Skip(skip).Take(request.PageSize).ToListAsync();

            bool hasNextPage = await query.Skip(skip + request.PageSize).AnyAsync();

            return (items, hasNextPage);
        }
    }
}
