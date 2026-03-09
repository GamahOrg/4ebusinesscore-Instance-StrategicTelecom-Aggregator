namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Congratulations.Congrats
{
    public class GetAllCongratsRequestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Lk_SendTo { get; set; }
        public int? CongratStatus { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsStopped { get; set; }
    }
}
