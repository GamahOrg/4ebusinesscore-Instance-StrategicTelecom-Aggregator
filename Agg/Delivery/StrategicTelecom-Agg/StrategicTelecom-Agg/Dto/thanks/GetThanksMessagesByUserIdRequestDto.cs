namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.thanks
{
    public class GetThanksMessagesByUserIdRequestDto
    {
        public Guid UserId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
