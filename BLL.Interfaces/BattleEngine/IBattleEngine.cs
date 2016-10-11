using BLL.Interfaces.BattleEngine.Executors;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.BattleEngine.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine
{
    public interface IBattleEngine
    {
         void RegisterPreExecutor(IExecutor exec);

         void RegisterPostExecutor(IExecutor exec);

         void RegisterValidator(IValidator exec);

        BattleResult Execute(IMessage message);


         void RegisterBattle(IBattle battle);

         void UnregisterBattle(string id);
    }
}
