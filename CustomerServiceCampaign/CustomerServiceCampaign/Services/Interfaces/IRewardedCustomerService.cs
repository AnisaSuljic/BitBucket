using CustomerServiceCampaign.DTOs;

namespace CustomerServiceCampaign.Services.Interfaces
{
    public interface IRewardedCustomerService
    {
        //logika za baratanje sa csv 
        List<ReportModel> CampaignResults(IFormFile file, int campaignId);
    }
}
