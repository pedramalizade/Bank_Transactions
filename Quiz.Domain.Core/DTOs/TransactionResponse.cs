namespace Quiz.Domain.Core.DTOs
{
    public class TransactionResponse
    {
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
        public DateTime TranceactionTime { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
