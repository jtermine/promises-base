using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Termine.Promises.Helpers
{
    public static class Functions
    {
        public static void CreateChaosInProperties<T>(T chaosObject)
        {
            var properties = typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in properties.Where(f => f.PropertyType == typeof (string)))
            {
                // Only work with strings
                if (p.PropertyType != typeof (string))
                {
                    continue;
                }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead)
                {
                    continue;
                }

                var mget = p.GetGetMethod(false);
                var mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null)
                {
                    continue;
                }
                if (mset == null)
                {
                    continue;
                }

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
                var random = new Random();

                var chaosString = new string(
                    Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());

                p.SetValue(chaosObject, chaosString, null);
            }

            foreach (var p in properties.Where(f => f.PropertyType == typeof (int)))
            {
                // Only work with strings
                if (p.PropertyType != typeof (int))
                {
                    continue;
                }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead)
                {
                    continue;
                }

                var mget = p.GetGetMethod(false);
                var mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null)
                {
                    continue;
                }
                if (mset == null)
                {
                    continue;
                }

                var randomInt = new Random();
                p.SetValue(chaosObject, randomInt.Next(), null);
            }

            foreach (var p in properties.Where(f => f.PropertyType == typeof (double)))
            {
                // Only work with strings
                if (p.PropertyType != typeof (double))
                {
                    continue;
                }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead)
                {
                    continue;
                }

                var mget = p.GetGetMethod(false);
                var mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null)
                {
                    continue;
                }
                if (mset == null)
                {
                    continue;
                }

                var randomInt = new Random();
                p.SetValue(chaosObject, Convert.ToDouble(randomInt.Next()), null);
            }

        }

        public static string GetFileFromResource(string resourceFileName)
        {

            var resourcePath = (from x in Assembly.GetExecutingAssembly().GetManifestResourceNames()
                                where x.ToLower().Contains(resourceFileName.ToLower())
                                select x).SingleOrDefault();

            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == default(Stream)) return string.Empty;

                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}