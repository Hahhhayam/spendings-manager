using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class MoneyFormat : BaseEntity
    {
        public string Name { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = null!;
    }
}
