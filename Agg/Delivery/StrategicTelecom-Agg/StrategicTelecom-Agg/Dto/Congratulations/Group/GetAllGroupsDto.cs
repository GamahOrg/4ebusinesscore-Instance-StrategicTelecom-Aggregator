namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Congratulations.Group
{
    public record GetAllGroupsDto
    (Guid Id,
    string NameAr,
    string NameEn,
    List<EmplyeeDto> employess,
    int CountOfEmployees);
}
