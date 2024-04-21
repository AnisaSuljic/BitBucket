namespace CustomerServiceCampaign.DTOs
{
    public class ReportModel
    {
        public string CustomerSSN { get; set; }
        public DateTime RewardDate { get; set; }
        public bool WasRewarded { get; set; }
        public bool PurchasedAfterCampaign { get; set; }

    }
}
