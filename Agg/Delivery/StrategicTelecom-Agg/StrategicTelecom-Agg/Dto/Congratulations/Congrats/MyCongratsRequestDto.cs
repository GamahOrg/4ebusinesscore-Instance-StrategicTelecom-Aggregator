namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Congratulations.Congrats
{
    public class MyCongratsRequestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}
