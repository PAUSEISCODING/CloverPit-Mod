#nullable enable
using HarmonyLib;

namespace CallumMods.CloverPit
{
    public static class Patcher
    {
        private static Harmony? _harmony;

        public static void ApplyPatches()
        {
            if (_harmony != null) return;
            _harmony = new Harmony(Plugin.GUID);

            // For now, do NOT patch anything automatically,
            // because we haven't identified the real method yet.
            // When ready, we'll call: _harmony.PatchAll(typeof(PayoutPatch));
        }

        public static void RemovePatches()
        {
            _harmony?.UnpatchSelf();
            _harmony = null;
        }
    }

    // 🚧 TEMPORARILY DISABLED:
    // When we discover the actual class/method, we'll uncomment and set:
    // [HarmonyPatch(typeof(RealClassName), "RealMethodName")]
    public static class PayoutPatch
    {
        // This is the postfix we'll use once the attribute above is enabled.
        // Adjust the signature (argument types) after we inspect the real method.
        public static void Postfix(ref float __result)
        {
            var mod = Plugin.Instance;
            if (mod == null || !mod.itemEnabled.Value) return;

            __result *= mod.memeMultiplier.Value;
        }
    }
}