namespace _4EPlatform_StrategicTelecom_Aggregator.Controllers
{
    public class ThanksController : BaseController
    {
        private readonly IThankService _thankService;

        public ThanksController(IThankService thankService)
        {
            _thankService = thankService;
        }
        [HttpGet("GetThanksMessagesByUserId")]
        public async Task<ResponseDto> GetThanksMessagesByUserId([FromQuery] GetThanksMessagesByUserIdRequestDto request)

            => await _thankService.GetThanksMessagesByUserId(request);

    }
}
