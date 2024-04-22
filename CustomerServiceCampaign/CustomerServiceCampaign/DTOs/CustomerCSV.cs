namespace CustomerServiceCampaign.DTOs
{
    public class CustomerCSV
    {
        public int Id { get; set; }
        public string CustomerSSN { get; set; }
        public DateTime RewardDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float PurchaseAmount { get; set; }
        public int PurchaseId { get; set; }
    }
}
