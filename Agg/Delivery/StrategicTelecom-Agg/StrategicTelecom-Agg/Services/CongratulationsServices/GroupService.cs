namespace _4EPlatform_StrategicTelecom_Aggregator.Services.CongratulationsServices
{
    public class GroupService : IGroupService
    {
        private readonly GroupProtoService.GroupProtoServiceClient _groupClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDto _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public GroupService(IStringLocalizer<SharedResource> stringLocalizer, UserProtoService.UserProtoServiceClient userClient, IResponseHelper responseHelper, GroupProtoService.GroupProtoServiceClient groupClient)
        {

            _stringLocalizer = stringLocalizer;
            _userClient = userClient;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDto();
            _groupClient = groupClient;
        }

        public async Task<ResponseDto> GetAllGroups(int PageSize, int PageIndex)
        {
            var list = _groupClient.GetAllGroups(new GetAllGroupsRequest() { PageIndex = PageIndex, PageSize = PageSize });
            if (list.GetAllGroupsProtoModel == null || !list.GetAllGroupsProtoModel.Any())
            {
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "ERROR Localizer");
            }
            var userIds = new RequestedUserList();
            if (list.GetAllGroupsProtoModel.Select(x => x.EmployeesProtoModels).Any())
            {
                foreach (var item in list.GetAllGroupsProtoModel.Select(x => x.EmployeesProtoModels))
                {
                    userIds.UserId.AddRange(item.Select(x => x.UserId));
                }
            }
            var allUserData = _userClient.GetListOfUsers(userIds);
            var dto = new List<GetAllGroupsDto>();
            dto.AddRange(list.GetAllGroupsProtoModel.Select(x => new GetAllGroupsDto(Guid.Parse(x.Id),
                x.NameAr ?? "",
                x.NameEn ?? "",
                x.EmployeesProtoModels.Select(x =>
                new EmplyeeDto(Guid.TryParse(x.UserId, out var userId) ? userId : Guid.Empty,
                allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower())
                .Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").
                FirstOrDefault(),
                allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower())
                .Select(x => x.UserName).
                FirstOrDefault()
                )).ToList(),
                x.CountOfEmployees)));
            _responseDTO.Result = dto;
            _responseDTO.Message = _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "ERROR Localizer";
            _responseDTO.PageIndex = list.PageIndex;
            _responseDTO.PageSize = list.PageSize;
            _responseDTO.TotalItems = list.TotalItems;
            _responseDTO.TotalPages = list.TotalPage;
            _responseDTO.StatusEnum = Helpers.Enums.StatusEnum.Success;
            return _responseDTO;
        }

        public async Task<ResponseDto> GetGroupById(Guid Id)
        {
            var entity = _groupClient.GetGrouById(new GetGrouByIdRequest()
            {
                Id = Id.ToString(),
            });

            if (entity.GroupProtoModel == null)
            {
                return _responseHelper.NotFound(_stringLocalizer[SharedResourceKeys.NotFound] ?? "ERROR Localizer");
            }
            var userIds = new RequestedUserList();
            userIds.UserId.AddRange(entity.GroupProtoModel.EmployeesProtoModels.Select(s => s.UserId));

            var allUserData = _userClient.GetListOfUsers(userIds);


            var dto = new GroupDto(
                Guid.Parse(entity.GroupProtoModel.Id),
                entity.GroupProtoModel.NameAr ?? "",
                entity.GroupProtoModel.NameEn ?? "",
                entity.GroupProtoModel.EmployeesProtoModels.Select(x =>
                new EmplyeeDto(Guid.TryParse(x.UserId, out var userId) ? userId : Guid.Empty,
                allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower()).Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").FirstOrDefault(),
                allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower())
                .Select(x => x.UserName).
                FirstOrDefault())).ToList());

            return _responseHelper.RetrievedSuccessfully(dto, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "ERROR Localizer");
        }
    }
}
