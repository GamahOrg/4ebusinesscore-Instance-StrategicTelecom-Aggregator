namespace _4EPlatform_StrategicTelecom_Aggregator.Interfaces
{
    public interface IThankService
    {
        Task<ResponseDto> GetThanksMessagesByUserId(GetThanksMessagesByUserIdRequestDto request);

    }
}
