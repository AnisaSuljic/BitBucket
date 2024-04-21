using AutoMapper;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;
using CustomerServiceCampaign.Services.Interfaces;

namespace CustomerServiceCampaign.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly IMapper _mapper;
        public CampaignService(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }


        public List<CampaignDTO> GetAll()
        {
            var campaigns = _campaignRepository.GetAll();

            return _mapper.Map<List<CampaignDTO>>(campaigns);
        }

        public CampaignDTO GetById(int id)
        {
            var campaignbyid = _campaignRepository.GetById(id);
            if (campaignbyid == null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found.");
            }
            return _mapper.Map<CampaignDTO>(campaignbyid);
        }

        public CampaignDTO Create(CampaignDTO campaign)
        {
            var newCampaign = _mapper.Map<Campaign>(campaign);
            var addedCampaign = _campaignRepository.Create(newCampaign);
            return _mapper.Map<CampaignDTO>(addedCampaign);
        }
        public CampaignDTO Update(int id, CampaignDTO campaign)
        {
            var existingCampaign = _campaignRepository.GetById(id);
            if (existingCampaign == null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found.");
            }

            existingCampaign.CampaignName = campaign.CampaignName;
            existingCampaign.StartDate = campaign.StartDate;
            existingCampaign.EndDate = campaign.EndDate;
            existingCampaign.Discount = campaign.Discount;

            var updatedCampaign = _campaignRepository.Update(existingCampaign);

            return _mapper.Map<CampaignDTO>(updatedCampaign);
        }

        public CampaignDTO Delete(int id)
        {
            var existingCampaign = _campaignRepository.GetById(id);
            if (existingCampaign == null)
            {
                throw new InvalidOperationException($"Campaign with id {id} not found.");
            }

            var deletedCampaign = _campaignRepository.Delete(existingCampaign);
            return _mapper.Map<CampaignDTO>(deletedCampaign);
        }
    }
}
