using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceCampaign.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<CampaignDTO>> GetAll()
        {
            try
            {
                var list = _campaignService.GetAll();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<CampaignDTO> GetById(int id)
        {
            try
            {
                var campaign = _campaignService.GetById(id);
                return Ok(campaign);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }
        
        [HttpPost("Create")]
        public ActionResult<CampaignDTO> Create(CampaignDTO campaign)
        {
            try
            {
                return _campaignService.Create(campaign);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }
        
        [HttpPut("Update/{id}")]
        public ActionResult<CampaignDTO> Update(int id, [FromBody] CampaignDTO campaignDto)
        {
            try
            {
                var updatedCampaign = _campaignService.Update(id, campaignDto);
                return Ok(updatedCampaign);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }
        
        [HttpDelete("Delete/{id}")]
        public ActionResult<CampaignDTO> Delete(int id)
        {
            try
            {
                var deletedCampaign = _campaignService.Delete(id);
                return Ok(deletedCampaign);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong, please try again.");
            }
        }
    }
}
