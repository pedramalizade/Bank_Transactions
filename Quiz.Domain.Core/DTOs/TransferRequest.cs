namespace Quiz.Domain.Core.DTOs
{
    public class TransferRequest
    {
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
    }
}
