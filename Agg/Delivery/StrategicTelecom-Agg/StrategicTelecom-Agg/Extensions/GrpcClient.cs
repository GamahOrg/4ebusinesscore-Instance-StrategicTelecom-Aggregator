
using _4EPlatform.Application.Protos;
using _4EPlatform_StrategicTelecom_Aggregator.GrpClientsConfig;
using ControlPanel.ORGHierararchy.Protos;
using StrategicTelecom_Aggregator.API.Protos;

namespace _4EPlatform_StrategicTelecom_Aggregator.Extensions
{
    public static class GrpcClients
    {



        #region ControlPanelAggregator

        public static void AddControlPanelAggGrpcClients(this IServiceCollection services, string url)
        {


        }
        #endregion

        public static void AddServiceOperationsGrpcClients(this IServiceCollection services, string url)
        {

        }
        public static void AddServiceOrgMangerGrpcClients(this IServiceCollection services, string url)
        {

        }
        public static void AddServiceOrgMemberGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClientWithClientCredentials<OrgProtoService.OrgProtoServiceClient>(url);

        }
        public static void AddServiceUserGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(url);
        }

        #region DynamicWorkflow.Aggregator
        public static void AddDynamicWorkfloGrpcClients(this IServiceCollection services, string url)
        {
        }
        #endregion

        #region Congratulations
        public static void AddServiceCongratulationsGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<GroupProtoService.GroupProtoServiceClient>(url);
            services.AddGrpcServiceClient<CongratsProtoService.CongratsProtoServiceClient>(url);
        }
        #endregion

    }
}