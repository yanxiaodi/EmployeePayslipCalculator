using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmployeePayslipCalculator.WPFApp.Utils
{
    public class SerializerHelper
    {

        public static SerializerHelper Create()
        {
            SerializerHelper result = new SerializerHelper();
            return result;
        }
        
        public static SerializerHelper Create(JsonSerializerSettings settings)
        {
            SerializerHelper result = new SerializerHelper();
            result.JsonSettings = settings;
            return result;
        }

        
        public JsonSerializerSettings JsonSettings 
        { 
            get
            {
                if(jsonSettings ==  null)
                {
                    jsonSettings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                }
                return jsonSettings;
            }
            set
            {
                jsonSettings = value;
            }
        }
        private JsonSerializerSettings jsonSettings = null;
        
        public string ToJson<T>(T serialObject)
        {
            return JsonConvert.SerializeObject(serialObject, jsonSettings);
        }

        public T FromJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, jsonSettings);
        }
    }
}
