using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using Diz.Jobs;
using EFT;
using JetBrains.Annotations;
using Mugnum.TarkovMods.ExpandedFpsLimit.Extensions;
using Mugnum.TarkovMods.ExpandedFpsLimit.Patches;
using System;
using UnityEngine;
using static GClass1706;

namespace Mugnum.TarkovMods.ExpandedFpsLimit
{
	/// <summary>
	/// Expanded FPS limit mod plugin.
	/// </summary>
	[BepInPlugin("com.mugnum.expandedfpslimit", "Mugnum-ExpandedFpsLimit", "1.2.0")]
	public class Plugin : BaseUnityPlugin
	{
		#region Constants

		/// <summary>
		/// Default max FPS limit.
		/// </summary>
		internal const int DefaultMaxFps = 360;

		/// <summary>
		/// Default min FPS limit.
		/// </summary>
		internal const int DefaultMinFps = 30;

		/// <summary>
		/// Default background FPS limit.
		/// </summary>
		internal const int DefaultBackgroundFps = 30;

		/// <summary>
		/// Minimum possible FPS value (protection against manual editing).
		/// </summary>
		internal const int MinimumPossibleFps = 1;

		/// <summary>
		/// Maximum possible FPS value (for config sliders).
		/// </summary>
		internal const int MaximumPossibleFps = 1000;

		#endregion Constants

		#region Fields: Internal

		/// <summary>
		/// Max framerate limit.
		/// </summary>
		internal static ConfigEntry<int> MaxFpsLimit;

		/// <summary>
		/// Min framerate limit.
		/// </summary>
		internal static ConfigEntry<int> MinFpsLimit;

		/// <summary>
		/// Use background framerate limit.
		/// </summary>
		internal static ConfigEntry<bool> IsUsingBackgroundLimit;

		/// <summary>
		/// Background framerate limit.
		/// </summary>
		internal static ConfigEntry<int> BackgroundFpsLimit;

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
			Application.focusChanged += OnFocusChanged;
		}

		/// <summary>
		/// Initializes configuration.
		/// </summary>
		private void InitializeConfig()
		{
			const string GeneralSectionName = "1. General";
			const string BackgroundSectionName = "2. Background limit";
			var acceptableRange = new AcceptableValueRange<int>(MinimumPossibleFps, MaximumPossibleFps);

			MaxFpsLimit = Config.Bind(GeneralSectionName,
				"Maximum FPS limit",
				DefaultMaxFps,
				new ConfigDescription("Sets max framerate limit in Graphics settings.",
					new AcceptableValueRange<int>(DefaultMinFps, MaximumPossibleFps),
					new ConfigurationManagerAttributes { Order = 1 }));

			MinFpsLimit = Config.Bind(GeneralSectionName,
				"Minimum FPS limit",
				DefaultMinFps,
				new ConfigDescription("Sets min framerate limit in Graphics settings.",
					new AcceptableValueRange<int>(MinimumPossibleFps, DefaultMinFps),
					new ConfigurationManagerAttributes { Order = 0 }));

			IsUsingBackgroundLimit = Config.Bind(BackgroundSectionName,
				"Enable background FPS limit",
				false,
				new ConfigDescription("Enables custom framerate limit when game is minimized.",
					null,
					new ConfigurationManagerAttributes { Order = 1 }));

			BackgroundFpsLimit = Config.Bind(BackgroundSectionName,
				"Background FPS limit",
				DefaultBackgroundFps,
				new ConfigDescription("Framerate limit when game is minimized.",
					new AcceptableValueRange<int>(MinimumPossibleFps, MaximumPossibleFps),
					new ConfigurationManagerAttributes { Order = 0 }));

			MaxFpsLimit.SettingChanged += OnFpsLimitChanged;
			MinFpsLimit.SettingChanged += OnFpsLimitChanged;
		}

		/// <summary>
		/// On config FPS limits changed.
		/// </summary>
		/// <param name="sender"> Initiator. </param>
		/// <param name="e"> Event arguments. </param>
		private static void OnFpsLimitChanged(object sender, EventArgs e)
		{
			var range = GetFpsLimitsRange();
			var limitsConfig = new GClass1708
			{
				MinFramerateLimit = range.Min,
				MaxFramerateGameLimit = range.Max,
				MaxFramerateLobbyLimit = range.Max
			};
			GameGraphicsClass.SetFramerateLimits(limitsConfig);
		}

		/// <summary>
		/// On game focus changed.
		/// </summary>
		/// <param name="isFocused"> Is game in focus (not minimized). </param>
		private static void OnFocusChanged(bool isFocused)
		{
			if (!IsUsingBackgroundLimit.Value)
			{
				return;
			}
			if (isFocused)
			{
				InvokeVanillaFramerateBehavior();
				return;
			}

			SetCustomFramerateLimit(BackgroundFpsLimit.Value);
		}

		/// <summary>
		/// Sets FPS limit using unmodded behavior.
		/// </summary>
		private static void InvokeVanillaFramerateBehavior()
		{
			var isInRaid = Singleton<AbstractGame>.Instantiated && Singleton<AbstractGame>.Instance.InRaid;

			if (Singleton<SharedGameSettingsClass>.Instantiated)
			{
				Singleton<SharedGameSettingsClass>.Instance.Graphics.Controller.ChangeFramerate(isInRaid);
			}
		}

		/// <summary>
		/// Sets FPS limit using custom behavior.
		/// </summary>
		/// <param name="fpsLimit"> FPS limit. </param>
		private static void SetCustomFramerateLimit(int fpsLimit)
		{
			Application.targetFrameRate = fpsLimit;

			if (Singleton<JobScheduler>.Instantiated)
			{
				Singleton<JobScheduler>.Instance.SetTargetFrameRate(fpsLimit);
			}
		}

		/// <summary>
		/// Returns range for FPS limit, with centralized validation.
		/// </summary>
		/// <returns> FPS limit range. </returns>
		internal static IntegerValueRange GetFpsLimitsRange()
		{
			var minFpsLimit = Math.Max(MinimumPossibleFps, Math.Min(MinFpsLimit.Value, MaxFpsLimit.Value));
			var maxFpsLimit = Math.Max(MinimumPossibleFps, Math.Max(MinFpsLimit.Value, MaxFpsLimit.Value));
			return new IntegerValueRange(minFpsLimit, maxFpsLimit);
		}

		#endregion Methods
	}
}
