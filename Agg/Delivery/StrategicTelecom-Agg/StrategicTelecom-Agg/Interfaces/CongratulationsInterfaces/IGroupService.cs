namespace _4EPlatform_StrategicTelecom_Aggregator.Interfaces.CongratulationsInterfaces
{
    public interface IGroupService
    {
        Task<ResponseDto> GetGroupById(Guid Id);
        Task<ResponseDto> GetAllGroups(int PageSize, int PageIndex);
    }
}
