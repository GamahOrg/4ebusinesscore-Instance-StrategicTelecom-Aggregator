using Grpc.Core;

namespace _4EPlatform_StrategicTelecom_Aggregator.Services.TokenService
{
	public interface ITokenService
	{
		Task<CallOptions> AcquireToken();
		Task<CallOptions> GetAccessTokenAsync();
	}
}
