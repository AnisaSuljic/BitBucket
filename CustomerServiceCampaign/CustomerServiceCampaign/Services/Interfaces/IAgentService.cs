using CustomerServiceCampaign.DTOs;

namespace CustomerServiceCampaign.Services.Interfaces
{
    public interface IAgentService
    {
        Task<RewardedCustomerDTO> RewardCustomerAsync(int campaignId, int customerId);

    }
}
