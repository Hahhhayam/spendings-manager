using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class TagTransaction: BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
