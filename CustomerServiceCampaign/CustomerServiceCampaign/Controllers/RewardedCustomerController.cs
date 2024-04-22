using CsvHelper;
using CsvHelper.Configuration;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Repositories.Interfaces;
using CustomerServiceCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;

namespace CustomerServiceCampaign.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RewardedCustomerController : Controller
    {
        private readonly IRewardedCustomerService _rewardedCustomerService;

        public RewardedCustomerController(IRewardedCustomerService rewardedCustomerService)
        {
            _rewardedCustomerService = rewardedCustomerService;
        }

        [HttpPost("UploadCSV")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCSV(IFormFile file, int campaignId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            var results = _rewardedCustomerService.CampaignResults(file, campaignId);

            return Ok(results);
        }
    }
}
