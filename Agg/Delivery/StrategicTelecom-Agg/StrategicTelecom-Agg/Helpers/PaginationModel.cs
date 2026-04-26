namespace _4EPlatform_StrategicTelecom_Aggregator.Helpers
{
    public class PaginationModel
    {
        public PaginationModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
