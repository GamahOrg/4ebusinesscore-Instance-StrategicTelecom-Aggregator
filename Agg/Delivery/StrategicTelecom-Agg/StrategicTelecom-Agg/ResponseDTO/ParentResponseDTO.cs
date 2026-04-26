using _4EPlatform_StrategicTelecom_Aggregator.Helpers.Enums;

namespace _4EPlatform_StrategicTelecom_Aggregator.ResponseDTO
{
    public abstract class ParentResponseDTO
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public string Message { get; set; }

        public StatusEnum StatusEnum { get; set; }
    }
}
