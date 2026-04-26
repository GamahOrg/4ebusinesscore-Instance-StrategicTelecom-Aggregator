namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllMedal
{
    public class GetAllMedalResponses
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MedalName { get; set; }
        public string? imagename { get; set; }
        public string? Imagepath { get; set; }
        public DateTime? GivenDate { get; set; }
        public DateTime? CreateOn { get; set; }
        public string UserId { get; set; }
        public bool IsOrg { get; set; }
        public string medalTypeName { get; set; } = string.Empty;
    }
}
