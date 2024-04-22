namespace CustomerServiceCampaign.DTOs
{
    public class ReportModel
    {
        public string CustomerSSN { get; set; }
        public int AgentId { get; set; }
        public int CampaignId { get; set; }
        public DateTime RewardDate { get; set; }
        public bool UsedReward { get; set; }
        public int? PurchaseId { get; set; }

    }
}
