namespace Quiz.Endpoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Check Card
        /// </summary>
        [HttpPost("Check")]
        public IActionResult CheckCard([FromBody] CheckCardRequest request)
        {
            var result = _cardService.CheckCard(request.CardNumber, request.Password);
            return Ok(result);
        }

        /// <summary>
        /// GetCardByNumber
        /// </summary>
        [HttpGet("{cardNumber}")]
        public IActionResult GetCardByNumber(string cardNumber)
        {
            var card = _cardService.GetCardByNumber(cardNumber);
            if (card == null)
                return NotFound("کارت یافت نشد.");

            return Ok(card);
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = _cardService.ChangePassword(request.CardNumber, request.OldPassword, request.NewPassword);
            if (!result)
                return BadRequest("تغییر رمز ناموفق بود (کارت وجود ندارد یا رمز فعلی اشتباه است).");

            return Ok("رمز عبور با موفقیت تغییر یافت.");
        }

        /// <summary>
        /// pdate Balance
        /// </summary>
        [HttpPut("UpdateBalance")]
        public IActionResult UpdateBalance([FromBody] UpdateBalanceRequest request)
        {
            var result = _cardService.UpdateBalance(request.CardNumber, request.Amount);
            if (!result)
                return BadRequest("آپدیت موجودی ناموفق بود (کارت وجود ندارد یا غیر فعال است).");

            return Ok("موجودی کارت با موفقیت به‌روزرسانی شد.");
        }

        /// <summary>
        /// GetHolderName
        /// </summary>
        [HttpGet("HolderName/{cardNumber}")]
        public IActionResult GetHolderName(string cardNumber)
        {
            if (_cardService.GetHolderNameCard(cardNumber, out var holderName))
                return Ok(new { HolderName = holderName });

            return NotFound("نام صاحب کارت پیدا نشد.");
        }

        /// <summary>
        /// IsCardValid
        /// </summary>
        [HttpGet("IsValid/{cardNumber}")]
        public IActionResult IsCardValid(string cardNumber)
        {
            var result = _cardService.IsCardValid(cardNumber);
            return Ok(result);
        }

        /// <summary>
        /// ReduceAmount
        /// </summary>
        [HttpPost("ReduceAmount")]
        public IActionResult ReduceAmount([FromBody] ReduceAmountRequest request)
        {
            var result = _cardService.ReduceAmount(request.Amount, request.SourceCardNumber, request.DestinationCardNumber);
            if (!result)
                return BadRequest("انتقال وجه ناموفق بود (موجودی کافی نیست یا کارت نامعتبر است).");

            return Ok("انتقال وجه با موفقیت انجام شد.");
        }
    }
}
