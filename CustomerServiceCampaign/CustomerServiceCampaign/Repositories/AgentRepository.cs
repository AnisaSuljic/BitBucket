using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;
using CustomerServiceCampaign.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CustomerServiceCampaign.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ComtradeContext _comtradeContext;
        public AgentRepository(ComtradeContext comtradeContext)
        {
            _comtradeContext = comtradeContext;
        }

        public async Task<Agent> GetAgentByUsernameAndPassword(string username, string password)
        {
            var entity = await _comtradeContext.Agents.FirstOrDefaultAsync(x => x.Username == username);

            if (entity == null)
                throw new Exception("Invalid username or password.");

            var hash = PasswordHash.GenerateHash(entity.PasswordSalt, password);

            if (hash != entity.PasswordHash)
                throw new Exception("Invalid username or password.");

            return entity;
        }

        public bool ReachedDailyLimit(int agentId, int campaignId)
        {
            var forms = _comtradeContext.RewardedCustomers.Where(x => x.AgentId == agentId && x.CampaignId == campaignId && x.RewardDate == DateTime.Today).Count();

            return forms >= 5 ? true : false;
        }

        public Agent GetByName(string name)
        {
            return _comtradeContext.Agents.FirstOrDefault(x => x.Username == name);
        }

    }
}
