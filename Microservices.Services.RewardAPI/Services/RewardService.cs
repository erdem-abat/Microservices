using Microservices.Services.RewardAPI.Data;
using Microservices.Services.RewardAPI.Message;
using Microservices.Services.RewardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Microservices.Services.RewardAPI.Services
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _dboptions;

        public RewardService(DbContextOptions<AppDbContext> dboptions)
        {
            _dboptions = dboptions;
        }
        public async Task UpdateReward(RewardMessage rewardMessage)
        {
            try
            {
                Reward reward = new()
                {
                    OrderId = rewardMessage.OrderId,
                    RewardsActivity = rewardMessage.RewardsActivity,
                    UserId = rewardMessage.UserId,
                    RewardsDate = DateTime.Now
                };                
                await using var _db = new AppDbContext(_dboptions);
                await _db.Rewards.AddAsync(reward);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
