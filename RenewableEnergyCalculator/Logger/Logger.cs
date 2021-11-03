using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Logger
{
    public interface ILog
    {
        void LogException(string message);
    }
}