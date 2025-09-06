namespace Quiz.Domain.Core.DTOs
{
    public class ChangePasswordRequest
    {
        public string CardNumber { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
