using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;          // <-- Needed for Harmony patches
using UnityEngine;

namespace CallumMods.CloverPit
{
    [BepInPlugin(GUID, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.callum.cloverpitmod";
        public const string Name = "CloverPit Meme Item (Starter)";
        public const string Version = "0.0.1";

        // We'll treat this as our “meme item” multiplier for now.
        internal static Plugin? Instance;
        internal ConfigEntry<bool>  itemEnabled     = null!;
        internal ConfigEntry<float> memeMultiplier  = null!;

        private void Awake()
        {
            Instance = this;

            // Settings appear in BepInEx/config and can be changed without rebuilding.
            itemEnabled    = Config.Bind("MemeItem", "Enabled",    true,  "Turn the meme item on/off.");
            memeMultiplier = Config.Bind("MemeItem", "Multiplier", 777f,  "Ridiculous multiplier applied to payouts.");

            // Install our Harmony patches so we can intercept/modify payout.
            Patcher.ApplyPatches();

            Logger.LogInfo($"{Name} {Version} loaded. Multiplier={memeMultiplier.Value}");
        }

        private void OnDestroy()
        {
            // Optional: cleanly remove patches if the plugin is ever unloaded.
            Patcher.RemovePatches();
            Logger.LogInfo($"{Name} {Version} unloaded.");
        }
    }
}
