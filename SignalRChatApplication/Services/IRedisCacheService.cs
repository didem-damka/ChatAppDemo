using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApplication.Services
{
    public interface IRedisCacheService
    {
        T Get<T>(string key);
        void Set(string key, object data);

        void Publish(string channel, object data);

    }
}
