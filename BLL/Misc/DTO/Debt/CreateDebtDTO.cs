namespace BLL.Misc.DTO.Debt
{
    public class CreateDebtDTO
    {
        public int TransactionId { get; set; }

        public int? PersonId { get; set; }

        public DateTime ToClose { get; set; }
    }
}
