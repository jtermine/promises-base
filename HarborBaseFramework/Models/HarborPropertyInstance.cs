namespace Termine.HarborData.Models
{
	public class HarborPropertyInstance
	{
		public string Name { get; set; }
		public byte[] Bytes { get; set; }
		public EnumPropertyInstanceState PropertyInstanceState { get; set; } = EnumPropertyInstanceState.None;
		public int PropertyVersion { get; set; } = -1;

		public enum EnumPropertyInstanceState
		{
			None = 0,
			Default = 1,
			Loaded = 2,
			Changed = 3
		}

	}
}
