using System;
using System.Collections.Generic;

namespace CustomerServiceCampaign.Models;

public partial class Campaign
{
    public int Id { get; set; }

    public string CampaignName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double Discount { get; set; }

    public virtual ICollection<RewardedCustomer> RewardedCustomers { get; set; } = new List<RewardedCustomer>();
}
