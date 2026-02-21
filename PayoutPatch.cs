using HarmonyLib;

namespace CallumMods.CloverPit
{
    // This class attaches to the plugin to install Harmony patches.
    public static class Patcher
    {
        private static Harmony? _harmony;

        public static void ApplyPatches()
        {
            if (_harmony != null) return;
            _harmony = new Harmony(Plugin.GUID);
            _harmony.PatchAll(typeof(PayoutPatch));
        }

        public static void RemovePatches()
        {
            _harmony?.UnpatchSelf();
            _harmony = null;
        }
    }

    // This is the actual patch.
    // IMPORTANT:
    // - You MUST replace "GameNamespace.PayoutCalculator" and "ComputeTotalPayout"
    //   with the real class/method names from the game's code.
    // - We'll talk about how to find those names just below.
    [HarmonyPatch(typeof(GameNamespace.PayoutCalculator), "ComputeTotalPayout")]
    public static class PayoutPatch
    {
        // Postfix runs after the original method.
        // If the original returns a float payout, __result is that number.
        public static void Postfix(ref float __result)
        {
            var mod = Plugin.Instance;
            if (mod == null) return;
            if (!mod.itemEnabled.Value) return;

            // Apply the meme multiplier
            __result *= mod.memeMultiplier.Value;
        }
    }
}