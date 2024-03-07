using DAL.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Currency : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Acronym { get; set; } = null!;

        public List<Transaction> Transactions { get; set; } = null!;
    }
}
