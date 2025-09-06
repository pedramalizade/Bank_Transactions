namespace Quiz.Endpoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest("ورودی اشتباه است");

            var user = _userService.Login(request.Username, request.Password);
            if (user == null)
                return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");

            return Ok(user);
        }

        /// <summary>
        /// Register
        /// </summary>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest("ورودی اشتباه است");

            var result = _userService.Register(user);
            if (!result)
                return BadRequest("این نام کاربری قبلاً ثبت شده است.");

            return Ok("ثبت‌نام با موفقیت انجام شد.");
        }

        /// <summary>
        /// RemoveCard
        /// </summary>
        [HttpPost("Add")]
        public IActionResult AddCard(int userId, [FromBody] Card card)
        {
            var user = InMemoryDb.OnlineUser;
            if (user == null)
                return Unauthorized("لطفا اول لاگین کنید.");

            if (card == null || string.IsNullOrEmpty(card.CardNumber) || string.IsNullOrEmpty(card.Password))
                return BadRequest("ورودی اطلاعات کارت اشتباه است");

            var result = _userService.AddCard(userId, card);
            if (!result)
                return BadRequest("افزودن کارت با خطا مواجه شد (ممکن است کارت تکراری باشد یا کاربر وجود نداشته باشد).");

            return Ok("کارت با موفقیت اضافه شد.");
        }

        /// <summary>
        /// Remove Card
        /// </summary>
        [HttpDelete("Remove/{cardNumber}")]
        public IActionResult RemoveCard(string cardNumber)
        {
            var result = _userService.RemoveCard(cardNumber);
            if (!result)
                return NotFound("کارت مورد نظر یافت نشد.");

            return Ok("کارت با موفقیت حذف شد.");
        }

        /// <summary>
        /// ShowCardBalance
        /// </summary>
        [HttpGet("Balance/{userId}")]
        public IActionResult ShowCardBalance(int userId)
        {
            var cards = _userService.ShowCardBalance(userId);
            if (cards == null || !cards.Any())
                return NotFound("هیچ کارتی برای این کاربر پیدا نشد.");

            return Ok(cards.Select(c => new
            {
                c.CardNumber,
                c.HolderName,
                c.Balance,
                c.IsActive
            }));
        }
    }
}
