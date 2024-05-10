using HarmonyLib;

namespace PotePat;

[HarmonyPatch(typeof(JesterAI))]
public class PoteJesterFart
{
    // Use custom music for the Jester
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    public static void Start(JesterAI __instance)
    {
        __instance.popGoesTheWeaselTheme = Plugin.AudioClips["fart"];
    }

    // Synchronize Jester timer with music length
    [HarmonyPatch("SetJesterInitialValues")]
    [HarmonyPostfix]
    public static void SetJesterInitialValues(JesterAI __instance)
    {
        __instance.popUpTimer = Plugin.AudioClips["fart"].length;
    }
}