using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApplication.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public T Get<T>(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var strObject = db.StringGet(key);
            if (strObject.IsNullOrEmpty)
                return default(T);
            else
                return JsonConvert.DeserializeObject<T>(strObject);
        }
        public void Set(string key, object value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var model = JsonConvert.SerializeObject(value);
            db.StringSetAsync(key, model);
        }
        public void Publish(string channel, object data)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var model = JsonConvert.SerializeObject(data);
            db.Publish(channel, model);
        }
    }
}
