namespace Mugnum.TarkovMods.ExpandedFpsLimit.Extensions
{
	/// <summary>
	/// Range for integer values.
	/// </summary>
	internal readonly struct IntegerValueRange
	{
		/// <summary>
		/// Min value.
		/// </summary>
		public int Min { get; }

		/// <summary>
		/// Max value.
		/// </summary>
		public int Max { get; }

		/// <summary>
		/// Constructor for min-max range.
		/// </summary>
		/// <param name="min"> Min value. </param>
		/// <param name="max"> Max value. </param>
		public IntegerValueRange(int min, int max)
		{
			Min = min;
			Max = max;
		}
	}
}
