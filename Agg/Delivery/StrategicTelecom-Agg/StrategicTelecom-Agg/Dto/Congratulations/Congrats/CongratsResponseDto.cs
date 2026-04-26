namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Congratulations.Congrats
{
    public class CongratDto
    {
        public Guid Id { get; set; }

        public string CategoryNameAr { get; set; }

        public string CategoryNameEn { get; set; }

        public string SubCategoryNameAr { get; set; }
        public string SubCategoryNameEn { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Lk_SendTo { get; set; }
        public string NameArLk_SendTo { get; set; }
        public string NameEnLk_SendTo { get; set; }
        public bool IsStopped { get; set; }
        public int CongratStatus { get; set; }
        public TemplateDto Template { get; set; }
        public List<GroupCongratDto> Group { get; set; }
        public List<userCongratsDto> Users { get; set; }
        public List<OrgCongratDto> Orgs { get; set; }
    }
    public class CongratByIdDto
    {
        public Guid Id { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryNameAr { get; set; }

        public string CategoryNameEn { get; set; }
        public Guid SubCategoryID { get; set; }
        public string SubCategoryNameAr { get; set; }
        public string SubCategoryNameEn { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Lk_SendTo { get; set; }
        public string NameArLk_SendTo { get; set; }
        public string NameEnLk_SendTo { get; set; }
        public bool? IsStopped { get; set; }
        public TemplateDto Template { get; set; }
        public List<GroupCongratDto> Group { get; set; }
        public List<userCongratsDto> Users { get; set; }
        public List<OrgCongratDto> Orgs { get; set; }
    }

    public class TemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FontFamily { get; set; }
        public string FontType { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public bool Job { get; set; }
        public string? UserJob { get; set; }
        public string? UserName { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryNameAr { get; set; }
        public string? CategoryNameEn { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string? SubCategoryNameAr { get; set; }
        public string? SubCategoryNameEn { get; set; }
        public string? Text { get; set; }
    }
    public class GroupCongratDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public List<userCongratsDto>? Members { get; set; }
        public class userCongratsDto
        {
            public Guid UserId { get; set; }
            public string? UserName { get; set; }
        }
        public class OrgCongratDto
        {
            public Guid OrgId { get; set; }
            public string? OrgNameAr { get; set; }
            public string? OrgNameEn { get; set; }
        }
    }
}
