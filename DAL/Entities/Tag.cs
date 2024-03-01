using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = null!;

        public List<TagTransaction> TagTransactions { get; set; } = null!;
    }
}
