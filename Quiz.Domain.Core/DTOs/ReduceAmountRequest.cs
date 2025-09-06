namespace Quiz.Domain.Core.DTOs
{
    public class ReduceAmountRequest
    {
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
        public double Amount { get; set; }
    }
}
