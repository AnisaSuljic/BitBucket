using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CustomerServiceCampaign.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpPost("RewardCustomer")]
        public async Task<ActionResult<RewardedCustomerDTO>> RewardCustomer(int campaignId, int customerId)
        {
            try
            {
                return await _agentService.RewardCustomerAsync(campaignId, customerId);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }
    }
}
