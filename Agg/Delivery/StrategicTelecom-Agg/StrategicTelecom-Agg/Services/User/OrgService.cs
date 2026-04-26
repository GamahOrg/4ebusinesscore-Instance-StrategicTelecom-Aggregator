namespace _4EPlatform_StrategicTelecom_Aggregator.Services.User
{
    public class OrgService : OrgProtoService.OrgProtoServiceBase
    {
        private readonly OrgProtoService.OrgProtoServiceClient _orgProtoService;


        public OrgService(OrgProtoService.OrgProtoServiceClient orgProtoService)
        {
            _orgProtoService = orgProtoService;
        }

        public override async Task<GetOrgHierarchyByIdsResponse> GetOrgHierarchysByIds(GetOrgHierarchysByIdsRequest request, ServerCallContext context)
        {
            return await _orgProtoService.GetOrgHierarchysByIdsAsync(request);
        }

    }
}
