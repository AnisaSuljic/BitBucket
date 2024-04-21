using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.Services.Interfaces
{
    public interface ICampaignService
    {
        List<CampaignDTO> GetAll();
        CampaignDTO GetById(int id);
        CampaignDTO Create(CampaignDTO campaign);
        CampaignDTO Update(int id, CampaignDTO campaign);
        CampaignDTO Delete(int id);
    }
}
