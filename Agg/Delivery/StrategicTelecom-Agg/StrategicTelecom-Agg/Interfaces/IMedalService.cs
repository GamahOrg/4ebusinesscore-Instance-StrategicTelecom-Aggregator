using _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetMedalOrgCount;

namespace _4EPlatform_StrategicTelecom_Aggregator.Interfaces
{
    public interface IMedalService
    {
        Task<ResponseDto> GetAllMedalbyCategoryId(GetAllMedalByCategoryIdQuery request);
        Task<ResponseDto> GetAllMedal(GetAllMedalRequestDto request);
        Task<ResponseDto> GetMedalOrgCount(GetMedalOrgCountDto request);


    }
}
