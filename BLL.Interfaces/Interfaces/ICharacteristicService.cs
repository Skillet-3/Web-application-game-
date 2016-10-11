using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.Models;

namespace BLL.Interfaces
{
    public interface ICharacteristicService
    {
        DetailedUserCharacteristics GetFullUserCharacteristics(string id);
        void SaveReward(Reward reward, DetailedUserCharacteristics status);
        void RestoreHP(string id);
    }
}