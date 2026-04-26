

using StrategicTelecom_Aggregator.API.Protos;

namespace _4EPlatform_StrategicTelecom_Aggregator.Services.Thanks
{
    public class ThankService : IThankService
    {
        private readonly UserProtoService.UserProtoServiceClient _userClient;
        private readonly ThanksMessageSevice.ThanksMessageSeviceClient _thanksClient;
        private ResponseDto _responseDTO;


        public ThankService(UserProtoService.UserProtoServiceClient userClient, ThanksMessageSevice.ThanksMessageSeviceClient thanksClient)
        {

            _userClient = userClient;
            _thanksClient = thanksClient;
            _responseDTO = new ResponseDto();
        }

        public async Task<ResponseDto> GetThanksMessagesByUserId(GetThanksMessagesByUserIdRequestDto request)
        {
            try
            {
                var response = _thanksClient.GetThanksMessagesByUserId(new GetThanksMessagesByUserIdRequest
                {
                    UserId = request.UserId.ToString(),
                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? 10
                });
                if (response.StatusEnum != (int)StatusEnum.Success)
                {
                    _responseDTO.Message = response.Message;
                    _responseDTO.StatusEnum = (StatusEnum)response.StatusEnum;
                    return _responseDTO;

                }

                var userIds = new RequestedUserList();

                userIds.UserId.AddRange(response.ThanksMessages.Select(x => x.UserId));

                var allUserData = _userClient.GetListOfUsers(userIds);

                var thanksMessagesDto = response.ThanksMessages.Select(x => new ThanksMessageResponseDto
                {
                    Id = Guid.Parse(x.Id),
                    ThanksMessage = x.ThnakMessage,
                    LikeCount = x.LikeCount,
                    IsLike = x.Islike,
                    Name = allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower()).Select(x => $"{x.FirstName} {x.SecondName} {x.ThirdName} {x.FourthName}").FirstOrDefault(),
                    job = allUserData?.UserData.Where(u => u.UserId.ToLower() == x.UserId.ToLower()).Select(x => x.JobNameAr).FirstOrDefault(),
                    Date = x.Date,

                }).ToList();
                _responseDTO.Result = thanksMessagesDto;
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
