using System;
using System.Collections.Generic;

namespace CustomerServiceCampaign.Models;

public partial class RewardedCustomer
{
    public int Id { get; set; }

    public string Ssn { get; set; } = null!;

    public int AgentId { get; set; }

    public int CampaignId { get; set; }

    public DateTime RewardDate { get; set; }

    public bool? UsedReward { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Campaign Campaign { get; set; } = null!;
}
