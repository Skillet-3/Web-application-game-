﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Messages
{
    public interface IMessageFactory
    {
        IMessage GetMessage(string battleId, string unit);
    }
}
