using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Models;

public partial class ComtradeContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //Agent
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

        //Campaign
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
        
        //RewardedCustomers
        modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 1,
                Ssn = "356-13-3072", 
                AgentId = 1,
                CampaignId = 1,
                RewardDate = DateTime.Parse("2024-04-21 00:00:00.0000000"),
                UsedReward = false
            }
        );

        modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 2,
                Ssn = "659-19-2190",
                AgentId = 1,
                CampaignId = 1,
                RewardDate = DateTime.Parse("2024-04-19 00:00:00.0000000"),
                UsedReward = false
            }
        );
        
        modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 3,
                Ssn = "850-53-6279",
                AgentId = 1,
                CampaignId = 1,
                RewardDate = DateTime.Parse("2024-04-20 00:00:00.0000000"),
                UsedReward = false
            }
        );

         modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 4,
                Ssn = "735-10-3297",
                AgentId = 1,
                CampaignId = 1,
                RewardDate = DateTime.Parse("2024-04-22 00:00:00.0000000"),
                UsedReward = false
            }
        );
        
        modelBuilder.Entity<RewardedCustomer>().HasData(
            new RewardedCustomer()
            {
                Id = 5,
                Ssn = "850-53-6566",
                AgentId = 1,
                CampaignId = 1,
                RewardDate = DateTime.Parse("2024-04-20 00:00:00.0000000"),
                UsedReward = false
            }
        );

    }

}
