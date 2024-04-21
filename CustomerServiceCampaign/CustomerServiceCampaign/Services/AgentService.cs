using AutoMapper;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;
using CustomerServiceCampaign.Services.Interfaces;
using System;
using System.Xml;

namespace CustomerServiceCampaign.Services
{
    public class AgentService : IAgentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAgentRepository _agentRepository;
        private readonly IRewardedCustomerRepository _rewardedCustomerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        private static readonly Random _random = new Random();


        public AgentService(IConfiguration configuration, IMapper mapper, IAgentRepository agentRepository, 
            IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IRewardedCustomerRepository rewardedCustomerRepository)
        {
            _agentRepository = agentRepository;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _rewardedCustomerRepository = rewardedCustomerRepository;
        }


        public async Task<RewardedCustomerDTO> RewardCustomerAsync(int campaignId, int customerId)
        {
            string username = _httpContextAccessor.HttpContext?.User.Identity?.Name;

            var agent = _agentRepository.GetByName(username);
            if (agent == null)
            {
                throw new ArgumentException($"Agent with name {username} not found.");
            }

            var agentId = agent.Id;

            bool isReachedDailyLimit = _agentRepository.ReachedDailyLimit(agentId, campaignId);

            if (isReachedDailyLimit)
            {
                throw new InvalidOperationException("You are reached daily limit.");
            }

            if (customerId == 0)
            {
                customerId = _random.Next(1, 201);
            }

            //get loyal customer
            
            string soapUrl = string.Format(_configuration["ExternalSOAP:URL"], customerId);

            HttpResponseMessage response = await _httpClient.GetAsync(soapUrl);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var customerSSN = GetSSNFromSoapResponse(responseBody);

            //check if custrmer already rewarder in selected campaign

            bool isAlreadyRewarded = _rewardedCustomerRepository.AlreadyRewarded(customerSSN, campaignId);

            if(isAlreadyRewarded)
            {
                throw new InvalidOperationException("Customer is already rewarded for this campaign.");
            }

            RewardedCustomer rewardedCustomer = new RewardedCustomer()
            {
                Ssn = customerSSN,
                AgentId = agentId,
                CampaignId = campaignId,
                RewardDate = DateTime.Today,
            };

            var result = _rewardedCustomerRepository.RewardCustomer(rewardedCustomer);

            return _mapper.Map<RewardedCustomerDTO>(result);
        }

        private string GetSSNFromSoapResponse(string soapResponse)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(soapResponse);

            // Create an XmlNamespaceManager to handle namespaces
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsMgr.AddNamespace("tempuri", "http://tempuri.org");

            // Use the XmlNamespaceManager when selecting nodes
            XmlNode ssnNode = xmlDoc.SelectSingleNode("//tempuri:SSN", nsMgr);

            string ssnValue = ssnNode?.InnerText;

            return ssnValue;
        }

    }
}
