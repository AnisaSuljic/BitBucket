using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.Repositories.Interfaces
{
    public interface ICampaignRepository
    {
        List<Campaign> GetAll();
        Campaign GetById(int id);
        Campaign Create(Campaign campaign);
        Campaign Update(Campaign campaign);
        Campaign Delete(Campaign campaign);
    }
}
