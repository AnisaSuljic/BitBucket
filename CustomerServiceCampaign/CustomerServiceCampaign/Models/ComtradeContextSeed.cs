using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Models;

public partial class ComtradeContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        var agent = new Agent()
        {
            Id = 1,
            FirstName = "Agent",
            LastName = "Test",
            Username = "agent",
        };

        agent.PasswordSalt = Security.PasswordHash.GenerateSalt();
        agent.PasswordHash = Security.PasswordHash.GenerateHash(agent.PasswordSalt, "agent");

        modelBuilder.Entity<Agent>().HasData(agent);

        modelBuilder.Entity<Campaign>().HasData(
            new Campaign()
            {
                Id = 1,
                CampaignName = "Campaign01", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(30),
                Discount = 10,
            }
        );
        
        modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 1,
                Ssn = "4sdfs56d9ed", 
                RewardDate = DateTime.Now,
                AgentId = 1,
                CampaignId = 1,
                UsedReward = false
            }
        );
        
    }

}
