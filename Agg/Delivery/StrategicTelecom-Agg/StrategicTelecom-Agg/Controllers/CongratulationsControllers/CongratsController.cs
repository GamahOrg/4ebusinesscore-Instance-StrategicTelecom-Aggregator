namespace _4EPlatform_StrategicTelecom_Aggregator.Controllers.CongratulationsControllers
{
    public class CongratsController(ICongratService _service) : BaseController
    {
        #region Getbyid
        [HttpGet("GetCongratById/{Id}")]
        public async Task<ResponseDTO.ResponseDto> GetCongratById(Guid Id)
        {
            return await _service.GetCongratByIdAsync(new GetByCongratIdRequestDto
            {
                Id = Id
            });
        }
        #endregion
        #region GetAll
        [HttpGet("GetAllCongrats")]
        public async Task<ResponseDTO.ResponseDto> GetAllCongrats(int pageSize, int pageIndex, string? search, Guid? SubCategoryId, int? congratStatus = -1,
            int lk_send = 0, DateTime? sendDate = null, DateTime? endDate = null, Guid? categoryId =null)
        {
            return await _service.GetAllCongrats(new GetAllCongratsRequestDto
            {
                PageNumber = pageIndex,
                PageSize = pageSize,
                Search = search,
                CongratStatus = congratStatus,
                Lk_SendTo = lk_send,
                SendDate = sendDate,
                EndDate = endDate,
                SubCategoryId = SubCategoryId,
                CategoryId = categoryId

            });
        }
        #endregion
        #region GetAll
        [HttpGet("GetAllMyCongrats")]
        public async Task<ResponseDTO.ResponseDto> GetAllMyCongrats(int pageSize, int pageIndex, string? search, Guid? subCategoryId)
        {
            return await _service.GetAllMyCongrats(new MyCongratsRequestDto
            {
                PageNumber = pageIndex,
                PageSize = pageSize,
                Search = search,
                SubCategoryId = subCategoryId
            });
        }
        #endregion
    }
}
