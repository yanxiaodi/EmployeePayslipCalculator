using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayslipCalculator.Service
{
    public static class Utils
    {
        public static int ConvertToInt(double d)
        {
            var result = 0;
            int.TryParse(Math.Round(d, 0, MidpointRounding.AwayFromZero).ToString(), out result);
            return result;
        }
    }
}
