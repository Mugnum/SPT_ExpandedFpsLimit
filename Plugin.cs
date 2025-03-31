using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using JetBrains.Annotations;
using Mugnum.ExpandedFpsLimit.Patches;
using System;

namespace Mugnum.ExpandedFpsLimit
{
	/// <summary>
	/// Expanded FPS limit mod plugin.
	/// </summary>
	[BepInPlugin("mugnum.ExpandedFpsLimit", "ExpandedFpsLimit", "1.0.1")]
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
		/// Logger.
		/// </summary>
		internal static ManualLogSource Log;
		
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
			Log = Logger;
			Logger.LogInfo("Expanded FPS limit: initializing.");

			try
			{
				InitializeConfig();
				new SetFramerateLimitsPatch().Enable();
			}
			catch (Exception ex)
			{
				Logger.LogError($"Expanded FPS limit: {nameof(Awake)} method. Error: {ex}");
				throw;
			}
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
