using System;
using System.Text;

namespace Termine.HarborData
{
    public static class Extensions
    {
	    public static decimal ConvertToDecimal(this byte[] bytes)
	    {
		    if (bytes == default(byte[])) return default(decimal);

			const int offset = 0;

			var i1 = BitConverter.ToInt32(bytes, offset);
			var i2 = BitConverter.ToInt32(bytes, offset + 4);
			var i3 = BitConverter.ToInt32(bytes, offset + 8);
			var i4 = BitConverter.ToInt32(bytes, offset + 12);

			return new decimal(new[] { i1, i2, i3, i4 });
		}

	    public static byte[] ConvertToBytes(this decimal value)
	    {
			var intArray = decimal.GetBits(value);
			var result = new byte[intArray.Length * sizeof(int)];
			Buffer.BlockCopy(intArray, 0, result, 0, result.Length);

			return result;
		}

		public static int ConvertToInt(this byte[] bytes)
		{
			return bytes == default(byte[]) ? default(int) : BitConverter.ToInt32(bytes, 0);
		}

	    public static byte[] ConvertToBytes(this int value)
		{
			var intBytes = BitConverter.GetBytes(value);

			return intBytes;
		}

		public static string ConvertToString(this byte[] bytes)
		{
			return bytes == default(byte[]) ? default(string) : Encoding.UTF8.GetString(bytes);
		}

	    public static byte[] ConvertToBytes(this string value)
		{
			var stringBytes = Encoding.UTF8.GetBytes(value);

			return stringBytes;
		}

		public static bool ConvertToBool(this byte[] bytes)
		{
			return bytes != default(byte[]) && BitConverter.ToBoolean(bytes, 0);
		}

		public static byte[] ConvertToBytes(this bool value)
		{
			var boolBytes = BitConverter.GetBytes(value);

			return boolBytes;
		}

		public static DateTime FromUnixTime(this int unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTime);
		}

		public static long ToUnixTime(this DateTime date)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64((date - epoch).TotalSeconds);
		}
	}
}
