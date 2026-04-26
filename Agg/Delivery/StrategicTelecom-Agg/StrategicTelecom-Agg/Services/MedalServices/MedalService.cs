

using _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetMedalOrgCount;

namespace _4EPlatform_StrategicTelecom_Aggregator.Services.MedalServices
{
    public class MedalService : IMedalService
    {
        private readonly MedalProtoService.MedalProtoServiceClient _medalClient;
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly OrgProtoService.OrgProtoServiceClient _orgProtoServiceClient;
        private readonly IResponseHelper _responseHelper;
        private ResponseDto _responseDTO;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;


        public MedalService(IStringLocalizer<SharedResource> stringLocalizer,
            IResponseHelper responseHelper,
                        UserProtoService.UserProtoServiceClient userClient,
                        MedalProtoService.MedalProtoServiceClient medalClient,
                        OrgProtoService.OrgProtoServiceClient orgProtoServiceClient)
        {
            _stringLocalizer = stringLocalizer;
            _responseHelper = responseHelper;
            _responseDTO = new ResponseDto();
            _userClient = userClient;
            _medalClient = medalClient;
            _orgProtoServiceClient = orgProtoServiceClient;
        }

        public async Task<ResponseDto> GetAllMedalbyCategoryId(GetAllMedalByCategoryIdQuery request)
        {
            try
            {
                var allMedals = _medalClient.GetAllMedalbyCategoryId(new GetAllMedalbyCategoryIdRequest
                {
                    CategoryId = request.CategoryId.ToString(),
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    Title = request.Title,

                });

                if (allMedals.AllMedals.Count() <= 0)
                {
                    _responseHelper.FailedToSave(_stringLocalizer[SharedResourceKeys.NotFound] ?? "ERROR Localizer");
                }

                //GET ALL USERS DATA
                var userIds = new RequestedUserList();

                userIds.UserId.AddRange(allMedals.AllMedals.Select(x => x.Owner));

                var allUserData = _userClient.GetListOfUsers(userIds);

                var allCauseStagesDto = GetAllMedalbyCategoryIdMapping.GetAllMedalbyCategoryId(allMedals, allUserData);
                allCauseStagesDto.CategoryName = allMedals.CategoryName;
                _responseDTO.Result = allCauseStagesDto;
                _responseDTO.PageSize = allMedals.PageSize;
                _responseDTO.PageIndex = allMedals.PageNumber;
                _responseDTO.TotalItems = allMedals.TotalItems;
                _responseDTO.TotalPages = allMedals.TotalPages;

                return _responseHelper.RetrievedSuccessfully(allCauseStagesDto, _stringLocalizer[SharedResourceKeys.RetrievedSuccessfully] ?? "ERROR Localizer", ref _responseDTO);
            }
            catch (Exception ex)
            {

                var message = ex?.InnerException?.Message ?? ex.Message;
                _responseDTO.Message = message;
                _responseDTO.StatusEnum = StatusEnum.Failed;
                return _responseDTO;


            }
        }
        public async Task<ResponseDto> GetAllMedal(GetAllMedalRequestDto request)
        {
            try
            {
                var response = _medalClient.GetAllMedal(new GetAllMedalRequest
                {

                    Search = request.Search ?? "",
                    Arrange = (int)request.arrange,
                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? 10,


                });

                if (response.StatusEnum != (int)StatusEnum.Success)
                {
                    _responseDTO.Message = response.Message;
                    _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
                    return _responseDTO;

                }

                //GET ALL USERS DATA
                var userIds = new RequestedUserList();

                userIds.UserId.AddRange(response.AllMedals.Select(x => x.UserId));

                var allUserData = _userClient.GetListOfUsers(userIds);


                var OrgIds = new GetOrgHierarchysByIdsRequest();

                OrgIds.OrgId.AddRange(response.AllMedals.Select(x => x.UserId));

                var allOrgData = _orgProtoServiceClient.GetOrgHierarchysByIds(OrgIds);

                var MedalDto = response.AllMedals.Select(x => new GetAllMedalResponses
                {
                    Id = Guid.Parse(x.Id),
                    MedalName = x.Name,
                    CreateOn = DateTime.TryParse(x.CreatedOn, out var date) ? date : null,
                    GivenDate = DateTime.TryParse(x.Date, out var enDate) ? enDate : null,
                    imagename = x.ImageName,
                    Imagepath = x.ImagePath,
                    medalTypeName = x.MedalTypeName ?? "Not Classification",
                    UserId = x.UserId,
                    IsOrg = x.IsOrg,
                    Name = x.Type == (int)Avalible_for.user ?


                    allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower()).Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").FirstOrDefault() ?? "" :

                   allOrgData.Orgs.Where(u => u.Id.ToLower() == x.UserId.ToLower()).Select(x => x.NameAr).FirstOrDefault() ?? "",
                }).ToList();
                _responseDTO.Result = MedalDto;
                _responseDTO.PageSize = response.PageSize;
                _responseDTO.PageIndex = response.PageNumber;
                _responseDTO.TotalItems = response.TotalItems;
                _responseDTO.TotalPages = response.TotalPages;
                _responseDTO.Message = response.Message;
                _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;

                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.StatusEnum = StatusEnum.Failed;
                return _responseDTO;

            }
        }


        public async Task<ResponseDto> GetMedalOrgCount(GetMedalOrgCountDto request)
        {
            try
            {
                var response = _medalClient.GetMedalOrgCount(new GetMedalOrgCountRequest
                {

                    Search = request.Search ?? "",

                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? 10,


                });

                if (response.StatusEnum != (int)StatusEnum.Success)
                {
                    _responseDTO.Message = response.Message;
                    _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
                    return _responseDTO;

                }

                ////GET ALL USERS DATA
                //var userIds = new RequestedUserList();

                //userIds.UserId.AddRange(response.AllMedals.Select(x => x.UserId));

                //var allUserData = _userClient.GetListOfUsers(userIds);


                var OrgIds = new GetOrgHierarchysByIdsRequest();

                OrgIds.OrgId.AddRange(response.MedalOrgCount.Select(x => x.OrgId));

                var allOrgData = _orgProtoServiceClient.GetOrgHierarchysByIds(OrgIds);

                var MedalDto = response.MedalOrgCount.Select(x => new MedalOrgCountResponsesDTO
                {
                    OrgId = Guid.Parse(x.OrgId),
                    Count = x.Count,

                    //  allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower()).Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").FirstOrDefault() ?? "" :

                    OrgName = allOrgData.Orgs.Where(u => u.Id.ToLower() == x.OrgId.ToLower()).Select(x => x.NameAr).FirstOrDefault() ?? "",
                }).ToList();
                _responseDTO.Result = MedalDto;
                _responseDTO.PageSize = response.PageSize;
                _responseDTO.PageIndex = response.PageNumber;
                _responseDTO.TotalItems = response.TotalItems;
                _responseDTO.TotalPages = response.TotalPages;
                _responseDTO.Message = response.Message;
                _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;

                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.StatusEnum = StatusEnum.Failed;
                return _responseDTO;

            }
        }




    }
}
