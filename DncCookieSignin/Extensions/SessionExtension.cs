using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DncCookieSignin.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static bool SessionExists(this ISession session,string key)
        {
            if (session.Get(key) != null)
            {
                return true;
            }
            return false;
        }
    }
}
