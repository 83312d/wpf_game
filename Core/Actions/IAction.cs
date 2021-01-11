using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actions
{
    public interface IAction
    {
        event EventHandler<string> OnActionExecute;
        void Execute(LivingBeing actor, LivingBeing target);
    }
}
