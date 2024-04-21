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

        public List<ReportModel> CampaignResults(int campaignId, List<CustomerCSV> customers, DateTime campaignEndDate)
        {

            var rewardedCustomers = _comtradeContext.RewardedCustomers.Where(x => x.CampaignId == campaignId).ToList();

            var report = (from reward in rewardedCustomers
                          join purchase in customers on reward.Ssn equals purchase.CustomerSSN into gj
                          from purchase in gj.DefaultIfEmpty()
                          select new ReportModel
                          {
                              CustomerSSN = reward.Ssn,
                              RewardDate = reward.RewardDate,
                              WasRewarded = purchase != null,
                              PurchasedAfterCampaign = purchase != null && purchase.PurchaseDate > campaignEndDate
                          }).ToList();

            var customersWithUsedReward = report.Where(r => r.PurchasedAfterCampaign).Select(r => r.CustomerSSN).ToList();

            foreach (var ssn in customersWithUsedReward)
            {
                var rewardedCustomer = rewardedCustomers.FirstOrDefault(r => r.Ssn == ssn && r.CampaignId == campaignId);
                if (rewardedCustomer != null)
                {
                    rewardedCustomer.UsedReward = true;
                    _comtradeContext.SaveChanges();
                }
            }

            return report;
        }
    }
}
