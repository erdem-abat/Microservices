using Microservices.Services.RewardAPI.Message;

namespace Microservices.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateReward(RewardMessage rewardMessage);
    }
}
