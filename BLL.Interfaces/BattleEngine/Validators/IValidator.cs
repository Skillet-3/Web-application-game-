using BLL.Interfaces.BattleEngine.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Validators
{
    public interface IValidator
    {
        bool Validate(IMessage message);
    }
}
