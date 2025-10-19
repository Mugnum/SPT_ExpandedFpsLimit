using BepInEx;
using BepInEx.Configuration;
using JetBrains.Annotations;
using Mugnum.TarkovMods.ExpandedFpsLimit.Patches;

namespace Mugnum.TarkovMods.ExpandedFpsLimit
{
	/// <summary>
	/// Expanded FPS limit mod plugin.
	/// </summary>
	[BepInPlugin("com.mugnum.expandedfpslimit", "Mugnum-ExpandedFpsLimit", "1.1.0")]
	public class Plugin : BaseUnityPlugin
	{
		#region Constants

		/// <summary>
		/// Default max framerate limit.
		/// </summary>
		internal const int DefaultMaxFramerate = 360;
		
		#endregion Constants

		#region Fields: Internal

		/// <summary>
		/// Max framerate limit.
		/// </summary>
		internal static ConfigEntry<int> MaxFpsLimit;

		#endregion Fields: Internal

		#region Methods

		/// <summary>
		/// Initializes the plugin.
		/// </summary>
		[UsedImplicitly]
		internal void Awake()
		{
			InitializeConfig();
			new SetFramerateLimitsPatch().Enable();
		}

		/// <summary>
		/// Initializes configuration.
		/// </summary>
		private void InitializeConfig()
		{
			const string GeneralSectionName = "General";
			MaxFpsLimit = Config.Bind(GeneralSectionName, "Maximum framerate limit", DefaultMaxFramerate,
				"Sets FPS limit in Graphics settings. Requires game restart.");
		}

		#endregion Methods
	}
}
