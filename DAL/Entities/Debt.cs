using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Abstractions;

namespace DAL.Entities
{
    public class Debt : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;

        [ForeignKey(nameof(Person))]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        [Required]
        public DateTime ToClose { get; set; }
    }
}
