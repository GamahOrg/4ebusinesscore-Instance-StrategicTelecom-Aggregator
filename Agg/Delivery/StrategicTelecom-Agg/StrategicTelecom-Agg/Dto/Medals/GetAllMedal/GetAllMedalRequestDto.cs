namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllMedal
{
    public class GetAllMedalRequestDto
    {
        public string? Search { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public arrange arrange { get; set; }

    }
    public enum arrange
    {
        OrderByAscending = 1,
        OrderByDescending = 2

    }
}
