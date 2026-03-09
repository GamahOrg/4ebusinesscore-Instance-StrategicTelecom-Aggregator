

namespace _4EPlatform_StrategicTelecom_Aggregator.Services.User
{
    public class UserService : UserProtoService.UserProtoServiceBase
    {
        private readonly UserProtoService.UserProtoServiceClient _userProtoServiceClient;

        public UserService(UserProtoService.UserProtoServiceClient userProtoServiceClient)
        {
            _userProtoServiceClient = userProtoServiceClient;
        }

        public override async Task<GetAllUsersByIdsResponse> GetAllUsersByIds(GetAllUsersByIdsRequest request, ServerCallContext context)
        {

            return await _userProtoServiceClient.GetAllUsersByIdsAsync(request);



        }
    }
}
