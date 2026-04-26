namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllCauseStages
{
    public class MedalsByCategoryIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { set; get; }
        public bool IsDraft { get; set; }
        public Guid Owner { get; set; }
        public int? TotalMedalsCount { get; set; }

        public string OwnerFullName { get; set; }
        public string? GivingTime { get; set; }

    }
}
