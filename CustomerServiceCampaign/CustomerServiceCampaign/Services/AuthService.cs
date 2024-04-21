using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Repositories.Interfaces;
using CustomerServiceCampaign.Services.Interfaces;

namespace CustomerServiceCampaign.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAgentRepository _agentRepository;

        public AuthService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<AgentDTO> Login(string username, string password)
        {
            var agentDto = new AgentDTO();

            var agent = await _agentRepository.GetAgentByUsernameAndPassword(username, password);

            agentDto.FirstName = agent.FirstName;
            agentDto.LastName = agent.LastName;
            agentDto.Username = agent.Username;

            return agentDto;
        }
    }
}
