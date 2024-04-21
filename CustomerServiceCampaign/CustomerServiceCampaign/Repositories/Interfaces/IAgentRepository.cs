using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.Repositories.Interfaces
{
    public interface IAgentRepository
    {
        Task<Agent> GetAgentByUsernameAndPassword(string username, string password);
        bool ReachedDailyLimit(int agentId, int campaignId);
        Agent GetByName(string name);
    }
}
