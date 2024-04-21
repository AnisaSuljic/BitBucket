using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.Repositories.Interfaces
{
    public interface IRewardedCustomerRepository
    {
        RewardedCustomer RewardCustomer(RewardedCustomer customer);
        bool AlreadyRewarded(string customerSSN, int campaignId);

        List<ReportModel> CampaignResults(int campaignId, List<CustomerCSV> customers, DateTime campaignEndDate);

    }
}
