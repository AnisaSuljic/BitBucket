using AutoMapper;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Campaign, CampaignDTO>().ReverseMap();
            //CreateMap<CampaignDTO, Campaign>()
            //    .ForMember(x=>x.RewardedCustomers)
            CreateMap<Agent, AgentDTO>().ReverseMap();
            CreateMap<RewardedCustomer, RewardedCustomerDTO>().ReverseMap();
            
        }
    }
}
