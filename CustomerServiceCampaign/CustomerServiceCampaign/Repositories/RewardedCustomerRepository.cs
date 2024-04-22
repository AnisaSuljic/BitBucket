using CustomerServiceCampaign.DTOs;
using CustomerServiceCampaign.Models;
using CustomerServiceCampaign.Repositories.Interfaces;

namespace CustomerServiceCampaign.Repositories
{
    public class RewardedCustomerRepository : IRewardedCustomerRepository
    {
        private readonly ComtradeContext _comtradeContext;
        public RewardedCustomerRepository(ComtradeContext comtradeContext)
        {
            _comtradeContext = comtradeContext;
        }

        public RewardedCustomer RewardCustomer(RewardedCustomer customer)
        {
            _comtradeContext.RewardedCustomers.Add(customer);
            _comtradeContext.SaveChanges();
            return customer;
        }

        public bool AlreadyRewarded(string customerSSN, int campaignId)
        {
            return _comtradeContext.RewardedCustomers.Where(x => x.Ssn == customerSSN && x.CampaignId == campaignId).Any();
        }

        public List<ReportModel> CampaignResults(int campaignId, List<CustomerCSV> customers)
        {

            var rewardedCustomers = _comtradeContext.RewardedCustomers.Where(x => x.CampaignId == campaignId).ToList();
            var report = new List<ReportModel>();

            foreach (var purchase in customers)
            {
                var rewardedCustomer = rewardedCustomers.FirstOrDefault(r => r.Ssn == purchase.CustomerSSN);
                if (rewardedCustomer != null && purchase.PurchaseDate > rewardedCustomer.RewardDate && !rewardedCustomer.UsedReward)
                {
                    rewardedCustomer.UsedReward = true;
                    _comtradeContext.SaveChanges();
                }
            }

            foreach (var rewardedCustomer in rewardedCustomers)
            {
                var customerPurchases = customers.Where(c => c.CustomerSSN == rewardedCustomer.Ssn && c.PurchaseDate > rewardedCustomer.RewardDate);

                var oldestPurchaseAfterRewardDate = customerPurchases.OrderBy(c => c.PurchaseDate).FirstOrDefault();

                var reportModel = new ReportModel
                {
                    CustomerSSN = rewardedCustomer.Ssn,
                    AgentId = rewardedCustomer.AgentId,
                    CampaignId = rewardedCustomer.CampaignId,
                    RewardDate = rewardedCustomer.RewardDate,
                    UsedReward = rewardedCustomer.UsedReward,
                    PurchaseId = oldestPurchaseAfterRewardDate?.PurchaseId 
                };

                report.Add(reportModel);
            }


            return report;
        }
    }
}
