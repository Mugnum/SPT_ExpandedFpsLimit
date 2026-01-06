using SPT.Reflection.Patching;
using System.Reflection;

namespace Mugnum.TarkovMods.ExpandedFpsLimit.Patches
{
	/// <summary>
	/// Patch for <see cref="GameGraphicsClass.SetFramerateLimits"/> method.
	/// </summary>
	internal class SetFramerateLimitsPatch : ModulePatch
	{
		/// <summary>
		/// Returns target method to override.
		/// </summary>
		/// <returns> Target method. </returns>
		protected override MethodBase GetTargetMethod()
		{
			return typeof(GameGraphicsClass).GetMethod(nameof(GameGraphicsClass.SetFramerateLimits));
		}

		/// <summary>
		/// Applies patch to method after it's execution.
		/// </summary>
		[PatchPostfix]
		private static void PatchPostfix()
		{
			var range = Plugin.GetFpsLimitsRange();
			GameGraphicsClass.MinFramerateLimit = range.Min;
			GameGraphicsClass.MaxFramerateGameLimit = range.Max;
			GameGraphicsClass.MaxFramerateLobbyLimit = range.Max;
		}
	}
}
