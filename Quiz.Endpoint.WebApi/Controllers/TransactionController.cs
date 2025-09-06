namespace Quiz.Endpoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("Transfer")]
        public IActionResult Transfer([FromBody] TransferRequest request)
        {
            var success = _transactionService.Transfer(request.SourceCardNumber, request.DestinationCardNumber, request.Amount, out string message);
            if (!success)
                return BadRequest(message);

            return Ok(message);
        }

        [HttpGet("History/{cardNumber}")]
        public IActionResult GetTransactions(string cardNumber)
        {
            var transactions = _transactionService.GetTransactions(cardNumber);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for this card.");

            var response = transactions.Select(t => new TransactionResponse
            {
                SourceCardNumber = t.SourceCardNumber,
                DestinationCardNumber = t.DestinationCardNumber,
                Amount = t.Amount,
                TranceactionTime = t.TranceactionTime,
                IsSuccessful = t.IsSuccessful
            }).ToList();

            return Ok(response);
        }
    }
}
