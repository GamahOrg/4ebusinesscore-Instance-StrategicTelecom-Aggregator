using _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllCauseStages;

namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllMedalsByCategoryId
{
    public class AllMedalsDto
    {
        public List<MedalsByCategoryIdDto> Medals { get; set; }
        public string? CategoryName { get; set; }
    }
}
