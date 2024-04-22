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
        
    }

}
