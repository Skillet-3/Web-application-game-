using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Messages
{
    public class JsonMessageBinder
    {
        Dictionary<string, Type> TypeDictionary = new Dictionary<string, Type>();
        public Dictionary<Type, string> TypeIdDictionary = new Dictionary<Type, string>();

        public IMessage Deserialize(string jsonData, string unit)
        {
            JObject jo = JObject.Parse(jsonData);
            string guid = jo["psx"].Value<string>();
            Type type;
            if(TypeDictionary.TryGetValue(guid, out type))
            {
                jo.Remove("psx");
                jo.Add("Unit", unit);
                return (IMessage)jo.ToObject(type);
            }
            throw new ArgumentException("Все плохо Карл", "message");
        }

        public string Serialize(IMessage message)
        {
            string guid;
            var type = message.GetType();
            if (TypeIdDictionary.ContainsKey(type))
            {
                guid = TypeIdDictionary[type];
                JObject jo = JObject.FromObject(message);
                jo.Add("psx", guid);
                jo.Remove("Unit");
                string json = jo.ToString();
                return json;
            }
            throw new ArgumentException("Все плохо Карл", "message");
        }



        public void Register<T>()
        {
            var type = typeof(T);
            if (!TypeIdDictionary.ContainsKey(type))
            {
                var guid = Guid.NewGuid().ToString();
                TypeDictionary.Add(guid, type);
                TypeIdDictionary.Add(type, guid);
            }
        }

        public void Unregister<T>()
        {
            string key;
            if(TypeIdDictionary.TryGetValue(typeof(T), out key))
            {
                TypeIdDictionary.Remove(typeof(T));
                TypeDictionary.Remove(key);
            }
        }
    }
}
