using System;
using System.Collections.Generic;

namespace CustomerServiceCampaign.Models;

public partial class Agent
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public virtual ICollection<RewardedCustomer> RewardedCustomers { get; set; } = new List<RewardedCustomer>();
}
