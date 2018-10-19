using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace DncCookieSignin.Util
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        private IMemoryCache _cache;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public MemoryCacheTicketStore(IMemoryCache cache)
        {
            //_cache = new MemoryCache(new MemoryCacheOptions());
            //_httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var guid = Guid.NewGuid();
            var key = KeyPrefix + guid.ToString();
            await RenewAsync(key, ticket);
            return key;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new MemoryCacheEntryOptions();
            var expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }
            options.SetSlidingExpiration(TimeSpan.FromSeconds(10)); // TODO: configurable.
            options.RegisterPostEvictionCallback(MyCallback, this);
            _cache.Set(key, ticket, options);
            _cache.Set("callbackMessage", "message from memory cache", TimeSpan.FromHours(1));
            return Task.FromResult(0);
        }

        private void MyCallback(object key, object value,EvictionReason reason, object state)
        {
            var message = $"Cache entry was removed : {reason}";
            
            _cache.Set("callbackMessage", message, TimeSpan.FromHours(1));
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            AuthenticationTicket ticket;
            _cache.TryGetValue(key, out ticket);
            return Task.FromResult(ticket);
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.FromResult(0);
        }
    }
}
