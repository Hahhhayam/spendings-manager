namespace BLL.DTO.Transaction
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int? CurrencyId { get; set; }

        public int? FormatId { get; set; }

        public DateTime Added { get; set; }
    }
}
