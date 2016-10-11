using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Messages
{
    public interface IMessage
    {
        string BattleId { get; }
        string MessageKey { get; }
        byte[] SrcData { get; }
        string Unit { get; }
    }
}
