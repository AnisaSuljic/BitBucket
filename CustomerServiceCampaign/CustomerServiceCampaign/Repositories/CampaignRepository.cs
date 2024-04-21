using AutoMapper;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ComtradeContext _comtradeContext;
        private readonly IMapper _mapper;

        public CampaignRepository(ComtradeContext comtradeContext, IMapper mapper)
        {
            _comtradeContext = comtradeContext;
            _mapper = mapper;
        }


        public List<Campaign> GetAll()
        {
            return _comtradeContext.Campaigns.Include(x=>x.RewardedCustomers).ToList();
        }

        public Campaign GetById(int id)
        {
            return _comtradeContext.Campaigns.Find(id);
        }

        public Campaign Create(Campaign campaign)
        {
            _comtradeContext.Campaigns.Add(campaign);
            _comtradeContext.SaveChanges();
            return campaign;
        }
        public Campaign Update(Campaign campaign)
        {
            _comtradeContext.Campaigns.Update(campaign);
            _comtradeContext.SaveChanges();

            return campaign;

        }

        public Campaign Delete(Campaign campaign)
        {
            _comtradeContext.Remove(campaign);
            _comtradeContext.SaveChanges();
            return campaign;

        }
    }
}
