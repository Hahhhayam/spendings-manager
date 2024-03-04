namespace BLL.DTO.Transaction
{
    public class CreateTransactionDTO
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int? CurrencyId { get; set; }

        public int? FormatId { get; set; }

    }
}
