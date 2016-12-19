using Newtonsoft.Json;
using Note.Infrastructure.Interfaces;

namespace Note.Services
{
    public class JsonConverterService : IJsonConverterService
    {
        /// <summary>
        /// This method is used to deserialize data
        /// </summary>
        /// <typeparam name="T">This is type of deserialized data</typeparam>
        /// <param name="str">This parameter is serialized data</param>
        /// <returns>This method return T</returns>
        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// This method is used to serialize data
        /// </summary>
        /// <param name="obj">This parameter is data to serialize</param>
        /// <returns>This method return string</returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
