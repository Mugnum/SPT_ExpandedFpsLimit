using SPT.Reflection.Patching;
using System.Reflection;

namespace Mugnum.TarkovMods.ExpandedFpsLimit.Patches
{
	/// <summary>
	/// Patch for GameGraphicsClass.SetFramerateLimits method.
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
		/// Applies to method after it's execution.
		/// </summary>
		[PatchPostfix]
		private static void PatchPostfix()
		{
			const int MinValue = 60;
			var fpsLimit = Plugin.MaxFpsLimit.Value;

			if (fpsLimit <= MinValue)
			{
				fpsLimit = MinValue;
			}

			GameGraphicsClass.MaxFramerateGameLimit = fpsLimit;
			GameGraphicsClass.MaxFramerateLobbyLimit = fpsLimit;
		}
	}
}
