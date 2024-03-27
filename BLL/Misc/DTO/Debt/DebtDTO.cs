namespace BLL.Misc.DTO.Debt
{
    public class DebtDTO
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        public int? PersonId { get; set; }

        public DateTime ToClose { get; set; }
    }
}
