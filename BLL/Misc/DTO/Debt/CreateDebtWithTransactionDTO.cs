using BLL.Misc.DTO.Transaction;

namespace BLL.Misc.DTO.Debt
{
    public class CreateDebtWithTransactionDTO
    {
        public CreateTransactionDTO Transaction { get; set; }

        public int? PersonId { get; set; }

        public DateTime ToClose { get; set; }
    }
}
