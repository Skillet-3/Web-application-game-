using BLL.Interfaces.BattleEngine.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BattleEngine.Messages
{
    public class MessageFactory : IMessageFactory
    {
        public IMessage GetMessage(string battleId, string unit)
        {
            return new SimpleExecutableMessage() { BattleId = battleId, Unit = unit };
        }


    }
}
