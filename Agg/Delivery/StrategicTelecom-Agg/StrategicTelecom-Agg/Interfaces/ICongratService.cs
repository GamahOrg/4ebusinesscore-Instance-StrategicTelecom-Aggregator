

namespace _4EPlatform_StrategicTelecom_Aggregator.Interfaces
{
    public interface ICongratService
    {
        Task<ResponseDto> GetCongratByIdAsync(GetByCongratIdRequestDto request);
        Task<ResponseDto> GetAllCongrats(GetAllCongratsRequestDto request);
        Task<ResponseDto> GetAllMyCongrats(MyCongratsRequestDto request);
    }
}
