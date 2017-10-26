using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayslipCalculator.WPFApp.Utils
{
    public sealed class ConfigHelper
    {
        private static readonly ConfigHelper instance = new ConfigHelper();

        public static ConfigHelper Instance
        {
            get
            {
                return instance;
            }
        }
        private ConfigHelper()
        {

        }


        private string serverUrl;
        public string ServerUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.serverUrl))
                {
                    return this.serverUrl;
                }
                else
                {
                    var serverUrlSetting = ConfigurationManager.AppSettings["serverUrl"];
                    if (serverUrlSetting != null)
                    {
                        this.serverUrl = serverUrlSetting;
                    }
                    return this.serverUrl;
                }
            }
        }


    }

}
