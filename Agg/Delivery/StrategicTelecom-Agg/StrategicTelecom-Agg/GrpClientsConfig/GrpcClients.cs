namespace _4EPlatform_StrategicTelecom_Aggregator.GrpClientsConfig
{

    public static class GrpcClients
    {
        //public static void AddOrgGrpcClients(this IServiceCollection services, string url)
        //{
        //    services.AddGrpcServiceClient<OrgProtoService.OrgProtoServiceClient>(url);
        //}

        #region ControlPanel.Aggregator
        public static void AddCPAggregatorGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<OrgMembersProtoService.OrgMembersProtoServiceClient>(url);
            services.AddGrpcServiceClient<OrgProtoService.OrgProtoServiceClient>(url);

        }
        #endregion
        public static void AddUserManagementGrpcClients(
  this IServiceCollection services,
  string UserManagmentUrl)
        {
            services.AddGrpcServiceClient<PermissionGroupingService.PermissionGroupingServiceClient>(UserManagmentUrl);
            //services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(UserManagmentUrl);
        }
        public static void AddUserGrpcClients(this IServiceCollection services, string url)
        {
            services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(url);
        }
        public static void AddMedalGrpcClients(this IServiceCollection services, string url)
        {

            services.AddGrpcServiceClient<MedalProtoService.MedalProtoServiceClient>(url);

        }
        public static void AddThanksGrpcClients(this IServiceCollection services, string url)
        {

            services.AddGrpcServiceClient<ThanksMessageSevice.ThanksMessageSeviceClient>(url);

        }

    }
}
