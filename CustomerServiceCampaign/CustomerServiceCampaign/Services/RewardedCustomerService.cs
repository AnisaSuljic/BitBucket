
using CsvHelper.Configuration;
using CsvHelper;
using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Services.Interfaces;
using System.Globalization;
using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;

namespace CustomerServiceCampaign.Services
{
    public class RewardedCustomerService : IRewardedCustomerService
    {
        private readonly IRewardedCustomerRepository _rewardedCustomerRepository;
        private readonly ICampaignRepository _campaignRepository;
        public RewardedCustomerService(IRewardedCustomerRepository rewardedCustomerRepository, ICampaignRepository campaignRepository)
        {
            _rewardedCustomerRepository = rewardedCustomerRepository;
            _campaignRepository = campaignRepository;
        }
        public List<ReportModel> CampaignResults(IFormFile file, int campaignId)
        {

            List<CustomerCSV> customers = new List<CustomerCSV>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";" // Postavljamo delimiter na tačka-zarez
            }))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var id = int.Parse(csv.GetField("Id"));
                    var customerSSN = csv.GetField("CustomerSSN");
                    var purchaseDate = csv.GetField("PurchaseDate");
                    var purchaseAmount = csv.GetField("PurchaseAmount");
                    var purchaseId = csv.GetField("PurchaseId");

                    var customer = new CustomerCSV
                    {
                        Id = id,
                        CustomerSSN = customerSSN,
                        PurchaseDate = DateTime.Parse(purchaseDate),
                        PurchaseAmount = float.Parse(purchaseAmount),
                        PurchaseId = int.Parse(purchaseId)
                    };

                    customers.Add(customer);
                }
            }
            //DateTime campaignEndDate = _campaignRepository.GetById(campaignId).EndDate;

            var reportResult = _rewardedCustomerRepository.CampaignResults(campaignId, customers);

            return reportResult;
        }
    }
}
