using _4EPlatform_StrategicTelecom_Aggregator.Services.MedalServices;
using _4EPlatform_StrategicTelecom_Aggregator.Services.User;

namespace _4EPlatform_StrategicTelecom_Aggregator.Extensions
{
    public static class GrpcServiceExtension
    {
        public static void AddgRPCServices(this WebApplication app)
        {
            app.MapGrpcService<UserService>();
            app.MapGrpcService<OrgService>();


        }
    }
}
