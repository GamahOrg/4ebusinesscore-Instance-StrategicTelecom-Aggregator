

using _4EPlatform_StrategicTelecom_Aggregator.Interfaces.CongratulationsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace _4EPlatform_StrategicTelecom_Aggregator.Controllers.CongratulationsControllers
{
    public class GroupsController : BaseController
    {
        private readonly IGroupService _service;

        public GroupsController(IGroupService service)
        {
            _service = service;
        }

        #region Getbyid
        [HttpGet("GetGroupById/{Id}")]
        public async Task<ResponseDTO.ResponseDto> GetGroupById(Guid Id)
        {
            return await _service.GetGroupById(Id);
        }
        #endregion

        #region GetAll
        [HttpGet("GetAllGroups")]
        public async Task<ResponseDTO.ResponseDto> GetAllGroups(int pageSize , int pageIndex)
        {
            return await _service.GetAllGroups(pageSize ,pageIndex);
        }
        #endregion
    }

}
