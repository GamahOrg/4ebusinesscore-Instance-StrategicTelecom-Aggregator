

namespace _4EPlatform_StrategicTelecom_Aggregator.Services.CongratsService
{
    public class CongratsService(CongratsProtoService.CongratsProtoServiceClient _congratsServiceClient,
        UserProtoService.UserProtoServiceClient _userServiceClient,
        OrgProtoService.OrgProtoServiceClient _orgProtoServiceClient,
         IHttpContextAccessor _httpContextAccessor) : ICongratService
    {
        private readonly ResponseDto _responseDTO = new ResponseDto();
        public Guid _loggedInUserId { get; set; } = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);

        public async Task<ResponseDto> GetCongratByIdAsync(GetByCongratIdRequestDto request)
        {
            var congratRequest = new GetCongratByIdRequest
            {
                Id = request.Id.ToString()
            };

            var response = await _congratsServiceClient.GetGrouByIdAsync(congratRequest);

            if (response.StatusEnum != (int)StatusEnum.Success)
            {
                _responseDTO.Message = "NotFound";
                _responseDTO.StatusEnum = StatusEnum.Failed;
                return _responseDTO;
            }

            // GET ALL USERS DATA
            var userIds = new RequestedUserList();

            if (response.CongratProtoModel?.UserIds.Count() != 0)
            {
                userIds.UserId.AddRange(response.CongratProtoModel?.UserIds);
            }

            if (response.CongratProtoModel?.GroupProtoModels.Select(x => x.UserIds).Count() > 0)
            {
                userIds.UserId.AddRange(response.CongratProtoModel.GroupProtoModels
                    .SelectMany(x => x.UserIds));
            }
            var allUserData = await _userServiceClient.GetListOfUsersAsync(userIds);

            // GET ALL ORGs DATA
            var orgIds = new GetOrgHierarchysByIdsRequest();

            if (response.CongratProtoModel?.OrgIds.Count() != 0)
            {
                orgIds.OrgId.AddRange(response.CongratProtoModel?.OrgIds);
            }

            var allOrgData = _orgProtoServiceClient.GetOrgHierarchysByIds(orgIds);

            var congratDto = new CongratByIdDto
            {

                Id = Guid.TryParse(response.CongratProtoModel?.Id, out var id) ? id : Guid.Empty,
                CategoryID = Guid.TryParse(response.CongratProtoModel?.CategoryID, out var CatId) ? CatId : Guid.Empty,
                CategoryNameAr = response.CongratProtoModel?.CategoryNameAr ?? "",
                CategoryNameEn = response.CongratProtoModel?.CategoryNameEn ?? "",
                SubCategoryID = Guid.TryParse(response.CongratProtoModel?.SubCategoryID, out var SubId) ? SubId : Guid.Empty,
                SubCategoryNameAr = response.CongratProtoModel?.SubCategoryNameAr ?? "",
                SubCategoryNameEn = response.CongratProtoModel?.SubCategoryNameEn ?? "",
                SendDate = DateTime.TryParse(response.CongratProtoModel?.SendDate, out var senDate) ? senDate : null,
                EndDate = DateTime.TryParse(response.CongratProtoModel?.EndDate, out var test) ? test : null,
                Lk_SendTo = response.CongratProtoModel?.LkSendTo ?? 0,
                NameArLk_SendTo = response.CongratProtoModel?.NameArLkSendTo ?? "",
                NameEnLk_SendTo = response.CongratProtoModel?.NameEnLkSendTo ?? "",
                IsStopped = response.CongratProtoModel?.IsStopped,

                Group = response.CongratProtoModel.GroupProtoModels.Select(x => new GroupCongratDto
                {
                    Id = Guid.Parse(x.GroupsIds),
                    NameAr = x.GroupNameAr,
                    NameEn = x.GroupNameEn,
                    Members = x.UserIds.Select(u => new userCongratsDto()
                    {
                        UserId = Guid.TryParse(u, out var userId) ? userId : Guid.Empty,
                        UserName = allUserData?.UserData
                    .Where(y => y.UserId.ToLower() == u.ToLower())
                    .Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}")
                    .FirstOrDefault(),
                    }).ToList()
                }).ToList(),
                Template = new TemplateDto
                {
                    Id = Guid.Parse(response.CongratProtoModel.TemplateProtoModels.Id),
                    Name = response.CongratProtoModel.TemplateProtoModels.Name,
                    Description = response.CongratProtoModel.TemplateProtoModels.Description,
                    FontFamily = response.CongratProtoModel.TemplateProtoModels.FontFamily,
                    FontType = response.CongratProtoModel.TemplateProtoModels.FontType,
                    FontColor = response.CongratProtoModel.TemplateProtoModels.FontColor,
                    FontSize = response.CongratProtoModel.TemplateProtoModels.FontSize,
                    ImageName = response.CongratProtoModel.TemplateProtoModels.ImageName,
                    ImageUrl = response.CongratProtoModel.TemplateProtoModels.ImageUrl,
                    Job = response.CongratProtoModel.TemplateProtoModels.Job,
                    Text = response.CongratProtoModel.TemplateProtoModels.Text,
                },
                Users = response.CongratProtoModel.UserIds.Select(x =>
                new userCongratsDto
                {
                    UserId = Guid.TryParse(x, out var userId) ? userId : Guid.Empty,
                    UserName = allUserData?.UserData
                    .Where(u => u.UserId.ToLower() == x.ToLower())
                    .Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}")
                    .FirstOrDefault(),
                }).ToList(),

                Orgs = response.CongratProtoModel.OrgIds.Select(x =>
                new OrgCongratDto
                {
                    OrgId = Guid.TryParse(x, out var orgId) ? orgId : Guid.Empty,
                    OrgNameAr = allOrgData?.Orgs
                    .Where(u => u.Id.ToLower() == x.ToLower())
                    .Select(x => x.NameAr)
                    .FirstOrDefault(),
                    OrgNameEn = allOrgData?.Orgs
                    .Where(u => u.Id.ToLower() == x.ToLower())
                    .Select(x => x.NameEn)
                    .FirstOrDefault(),
                }).ToList()

            };

            _responseDTO.Result = congratDto;
            _responseDTO.Message = response.Message;
            _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;

            return _responseDTO;
        }

        public async Task<ResponseDto> GetAllCongrats(GetAllCongratsRequestDto request)
        {
            var response = _congratsServiceClient.GetAllCongrats(new GetAllCongratsRequest
            {
                PageSize = request.PageSize != 0 ? request.PageSize : 10,
                PageNumber = request.PageNumber != 0 ? request.PageNumber : 1,
                Search = request.Search ?? "",
                LkSendTo = request.Lk_SendTo ?? default,
                CongratStatus = request.CongratStatus ?? -1,
                SendDate = request.SendDate.ToString() ?? "",
                EndDate = request.EndDate.ToString() ?? "",
                SubCategoryId = request.SubCategoryId.ToString() ?? "",
                CategoryId = request.CategoryId.ToString() ?? "",
            }); ;
            if (response.StatusEnum != (int)StatusEnum.Success)
            {
                _responseDTO.Message = response.Message;
                _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
                return _responseDTO;
            }

            // GET ALL USERS DATA
            var userIds = new RequestedUserList();
            if (response.CongratProtoModel.Select(x => x.UserIds).Any())
            {
                foreach (var item in response.CongratProtoModel.Select(x => x.UserIds))
                {
                    userIds.UserId.AddRange(item.Select(x => x));
                }
            }

            var allUserData = await _userServiceClient.GetListOfUsersAsync(userIds);

            // GET ALL ORGs DATA
            var orgIds = new GetOrgHierarchysByIdsRequest();

            if (response.CongratProtoModel.Select(x => x.OrgIds).Any())
            {
                foreach (var item in response.CongratProtoModel.Select(x => x.OrgIds))
                {
                    orgIds.OrgId.AddRange(item.Select(x => x));
                }
            }

            var allOrgData = _orgProtoServiceClient.GetOrgHierarchysByIds(orgIds);

            var congratDto = response.CongratProtoModel.Select(x => new CongratDto
            {
                Id = Guid.Parse(x.Id),
                CategoryNameAr = x.CategoryNameAr,
                CategoryNameEn = x.CategoryNameEn,

                SubCategoryNameAr = x.SubCategoryNameAr,
                SubCategoryNameEn = x.SubCategoryNameEn,
                SendDate = DateTime.TryParse(x.SendDate, out var date) ? date : null,
                EndDate = DateTime.TryParse(x.EndDate, out var enDate) ? enDate : null,
                Lk_SendTo = x.LkSendTo,
                NameArLk_SendTo = x.NameArLkSendTo,
                NameEnLk_SendTo = x.NameEnLkSendTo,
                IsStopped = x.IsStopped,
                CongratStatus = x.CongratStatus,
                Template = new TemplateDto
                {
                    Id = Guid.Parse(x.TemplateProtoModels.Id),
                    Name = x.TemplateProtoModels.Name ?? "",
                    Description = x.TemplateProtoModels.Description ?? "",
                    FontFamily = x.TemplateProtoModels.FontFamily ?? "",
                    FontType = x.TemplateProtoModels.FontType ?? "",
                    FontColor = x.TemplateProtoModels.FontColor ?? "",
                    FontSize = x.TemplateProtoModels.FontSize,
                    ImageName = x.TemplateProtoModels.ImageName ?? "",
                    ImageUrl = x.TemplateProtoModels.ImageUrl ?? "",
                    Job = x.TemplateProtoModels.Job,
                    Text = x.TemplateProtoModels.Text ?? "",
                },
                Group = x.GroupProtoModels.Select(x => new GroupCongratDto
                {
                    Id = Guid.Parse(x.GroupsIds),
                    NameAr = x.GroupNameAr,
                    NameEn = x.GroupNameEn
                }).ToList(),
                Users = x.UserIds.Select(x =>
                new userCongratsDto
                {
                    UserId = Guid.TryParse(x, out var userId) ? userId : Guid.Empty,
                    UserName = allUserData?.UserData
                    .Where(u => u.UserId.ToLower() == x.ToLower())
                    .Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}")
                    .FirstOrDefault(),
                }).ToList(),

                Orgs = x.OrgIds.Select(x =>
                new OrgCongratDto
                {
                    OrgId = Guid.TryParse(x, out var orgId) ? orgId : Guid.Empty,
                    OrgNameAr = allOrgData?.Orgs
                    .Where(u => u.Id.ToLower() == x.ToLower())
                    .Select(x => x.NameAr)
                    .FirstOrDefault(),
                    OrgNameEn = allOrgData?.Orgs
                    .Where(u => u.Id.ToLower() == x.ToLower())
                    .Select(x => x.NameEn)
                    .FirstOrDefault(),
                }).ToList()

            }).ToList();

            _responseDTO.Result = congratDto;
            _responseDTO.Message = response.Message;
            _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
            _responseDTO.PageIndex = response.PageNumber;
            _responseDTO.PageSize = response.PageSize;
            _responseDTO.TotalItems = response.TotalItems;
            _responseDTO.TotalPages = response.TotalPages;

            return _responseDTO;
        }

        public async Task<ResponseDto> GetAllMyCongrats(MyCongratsRequestDto request)
        {
            var response = _congratsServiceClient.GetAllMyCongrats(new GetAllMyCongratsRequest()
            {

                PageSize = request.PageSize != 0 ? request.PageSize : 10,
                PageNumber = request.PageNumber != 0 ? request.PageNumber : 1,
                Search = request.Search ?? "",
                SubCategoryId = request.SubCategoryId.ToString()
            });

            if (response.StatusEnum != (int)StatusEnum.Success)
            {
                _responseDTO.Message = response.Message;
                _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
                return _responseDTO;
            }
            var userData = _userServiceClient.GetUserById(new RequestGetUserById() { UserId = _loggedInUserId.ToString() });

            var congratDto = response.MyCongratProtoModels.Select(x => new GetAllMyCongratsResponseDto
            {
                Id = Guid.Parse(x.Id),
                TemplateDto = x.TemplateProtoModels.Select(t => new TemplateDto
                {
                    Id = Guid.Parse(t.Id),
                    Name = t.Name ?? "",
                    Description = t.Description ?? "",
                    FontFamily = t.FontFamily ?? "",
                    FontType = t.FontType ?? "",
                    FontColor = t.FontColor ?? "",
                    FontSize = t.FontSize,
                    ImageName = t.ImageName ?? "",
                    ImageUrl = t.ImageUrl ?? "",
                    Job = t.Job,
                    UserName = t.UserName ?? $"{userData.FirstName} {userData.SecondName} {userData.ThirdName} {userData.FourthName}",
                    UserJob = !string.IsNullOrEmpty(t.JobName) ? t.JobName : userData.JobNameAr,
                    CategoryId = Guid.TryParse(t.CategoryID, out var CategoryId) ? CategoryId : Guid.Empty,
                    CategoryNameAr = t.CategoryNameAr,
                    CategoryNameEn = t.CategoryNameEn,
                    SubCategoryId = Guid.TryParse(t.SubCategoryId, out var SubCategoryId) ? SubCategoryId : Guid.Empty,
                    SubCategoryNameAr = t.SubCategoryNameAr,
                    SubCategoryNameEn = t.SubCategoryNameEn,
                }).ToList()
            }).ToList();

            _responseDTO.Result = congratDto;
            _responseDTO.Message = response.Message;
            _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
            _responseDTO.PageIndex = response.PageNumber;
            _responseDTO.PageSize = response.PageSize;
            _responseDTO.TotalItems = response.TotalItems;
            _responseDTO.TotalPages = response.TotalPages;

            return _responseDTO;
        }
    }
}
