namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllCauseStages
{
    public class GetAllMedalByCategoryIdQuery
    {
        public string? Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Guid CategoryId { get; set; }

    }
}
