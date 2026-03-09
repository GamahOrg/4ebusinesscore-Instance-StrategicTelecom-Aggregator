namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Congratulations.Group
{
    public record GroupDto(Guid Id, string NameAr, string NameEn, List<EmplyeeDto> Employees);
    public record EmplyeeDto (Guid UserId , string UserName ,string UserNameEn);
}
