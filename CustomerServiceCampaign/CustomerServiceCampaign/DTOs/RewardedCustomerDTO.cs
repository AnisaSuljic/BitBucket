namespace CustomerServiceCampaign.DTOs
{
    public class RewardedCustomerDTO
    {
        public string Ssn { get; set; } = null!;

        public int AgentId { get; set; }

        public int CampaignId { get; set; }

        public DateTime RewardDate { get; set; }

        public bool? UsedReward { get; set; }
    }
}
