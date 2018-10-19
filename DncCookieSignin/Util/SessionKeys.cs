namespace DncCookieSignin.Util
{
    public class SessionKeys
    {
        private static readonly string _prefix = "session.";
        public static readonly string UserLoginModel = "UserLoginModel";

        public static string SessionKey(string sessionKey)
        {
            return _prefix + sessionKey;
        }
    }
}
