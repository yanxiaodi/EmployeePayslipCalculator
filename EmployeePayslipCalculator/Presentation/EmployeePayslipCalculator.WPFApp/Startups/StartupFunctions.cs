using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EmployeePayslipCalculator.Service;

namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static List<Action> AllConfig;

        public static Action CreateAndAddToAllConfig(this Action action)
        {
            if (AllConfig == null)
            {
                AllConfig = new List<Action>();
            }
            AllConfig.Add(action);
            return action;
        }
        public static void RunAllConfig()
        {
            Services.ServiceLocator.Instance.Register<PayslipCalculatorService>(new PayslipCalculatorService());
            if (AllConfig == null) return;
            foreach (var item in AllConfig)
            {
                item();
            }

        }

    }
}
