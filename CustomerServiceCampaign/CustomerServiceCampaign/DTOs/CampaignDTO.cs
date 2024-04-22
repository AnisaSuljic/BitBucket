using CustomerServiceCampaign.Models;

namespace CustomerServiceCampaign.DTOs
{
    public class CampaignDTO
    {
        public string CampaignName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Discount { get; set; }

    }
}
