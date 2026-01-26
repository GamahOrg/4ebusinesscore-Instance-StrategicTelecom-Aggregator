using _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetMedalOrgCount;

namespace _4EPlatform_StrategicTelecom_Aggregator.Controllers
{

    public class MedalController : BaseController
    {
        private readonly IMedalService _medalService;
        public MedalController(IMedalService medalService)
        {
            _medalService = medalService;
        }


        [HttpGet("GetAllMedalByCategoryId")]
        public async Task<ResponseDto> GetAllCauseStages([FromQuery] GetAllMedalByCategoryIdQuery request)

            => await _medalService.GetAllMedalbyCategoryId(request);


        [HttpGet("GetAllMedal")]
        public async Task<ResponseDto> GetAllMedal([FromQuery] GetAllMedalRequestDto request)

            => await _medalService.GetAllMedal(request);

        [HttpGet("GetMedalOrgCount")]
        public async Task<ResponseDto> GetMedalOrgCount([FromQuery] GetMedalOrgCountDto request)

    => await _medalService.GetMedalOrgCount(request);


    }
}

