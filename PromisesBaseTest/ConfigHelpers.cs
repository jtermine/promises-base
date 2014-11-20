using Microsoft.Win32;

namespace Termine.Promises.Base.Test
{
    public static class ConfigHelpers
    {
        public static string GetRedisConnString()
        {
            var myKey = Registry.CurrentUser.OpenSubKey(@"Software\Termine\PromiseBaseTest", false);
            if (myKey == null) return string.Empty;
            return (string)myKey.GetValue("RedisConnString");
        }
    }
}
