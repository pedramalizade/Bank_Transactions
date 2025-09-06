namespace Quiz.Domain.Core.DTOs
{
    public class UpdateBalanceRequest
    {
        public string CardNumber { get; set; }
        public float Amount { get; set; }
    }
}
