using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class Transaction : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName="Money")]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(Currency))]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        [ForeignKey(nameof(Format))]
        public int? FormatId { get; set; }
        public MoneyFormat? Format { get; set; }

        [Required]
        public DateTime Added { get; set; }

        public List<TagTransaction> TagTransactions { get; set; } = null!;

        public List<Debt> Debts { get; set; } = null!;
    }
}
