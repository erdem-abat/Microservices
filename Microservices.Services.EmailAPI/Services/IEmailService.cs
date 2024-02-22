using Microservices.Services.EmailAPI.Message;
using Microservices.Services.EmailAPI.Models.Dto;

namespace Microservices.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardMessage rewardMessage);
    }
}
