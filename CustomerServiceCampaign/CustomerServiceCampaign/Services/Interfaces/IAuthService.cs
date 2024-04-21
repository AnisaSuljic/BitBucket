using CustomerServiceCampaign.DTOs;

namespace CustomerServiceCampaign.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AgentDTO> Login(string username, string password);
    }
}
