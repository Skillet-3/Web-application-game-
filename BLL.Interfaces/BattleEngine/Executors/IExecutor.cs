using BLL.Interfaces.BattleEngine.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Executors
{
    public interface IExecutor
    {
        void Execute(IMessage message, IBattle battle);
    }
}
