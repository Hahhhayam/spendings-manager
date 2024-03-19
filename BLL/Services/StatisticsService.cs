using System.ComponentModel.DataAnnotations;
using BLL.DTO;
using BLL.DTO.Transaction;
using DAL.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class StatisticsService
    {
        private readonly SMDbContext context;

        public StatisticsService(SMDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TransactionIncludedDTO> GetFiltered(FilterDTO filter)
        {
            Validator.ValidateObject(filter, new ValidationContext(filter));

            var query = this.context.Transactions
                .Include(t => t.TagTransactions)
                    .ThenInclude(t => t.Tag)
                .AsNoTracking();

            if (filter.LowerDate != null)
            {
                query = query.Where(t => t.Added >= filter.LowerDate);
            }

            if (filter.UpperDate != null)
            {
                query = query.Where(t => t.Added <= filter.UpperDate);
            }

            foreach (DTO.Tag.TagDTO tag in filter.Tags)
            {
                query = query.Where(t => t.TagTransactions.Select(tT => tT.Tag).Any(t => t.Id == tag.Id));
            }

            if (filter.Currency != null)
            {
                query = query.Where(t => t.CurrencyId == filter.Currency.Id);
            }

            return query
                .ProjectToType<TransactionIncludedDTO>()
                .ToList();
        }
    }
}
