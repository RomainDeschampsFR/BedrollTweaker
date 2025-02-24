using Il2CppTLD.IntBackedUnit;

namespace BedrollTweaker
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal class GEAR_BedRoll
    {
        private static void Postfix(GearItem __instance)
        {
            if (Utilities.NormalizeName(__instance.name) == "GEAR_BedRoll")
            {
                if (Settings.settings.tweakBedroll == Choice.Custom)
                {
                    __instance.m_Bed.m_WarmthBonusCelsius = Settings.settings.bedrollWarmth;
                    __instance.WeightKG = ItemWeight.FromKilograms(Settings.settings.bedrollWeight);
                    //MelonLogger.Msg("tweakBedroll:Custom");
                    //MelonLogger.Msg($"[SETTING] : bedrollWarmth:{Settings.settings.bedrollWarmth}, bedrollWeight:{Settings.settings.bedrollWeight}");
                    //MelonLogger.Msg($"[ACTUAL]  : bedrollWarmth:{__instance.m_Bed.m_WarmthBonusCelsius}, bedrollWeight:{__instance.WeightKG}");
                }

                if (Settings.settings.bedrollDecay == Choice.Custom)
                {
                    __instance.m_GearItemData.m_DailyHPDecay = Settings.settings.bedrollDecayDaily;
                    if (__instance.m_DegradeOnUse)
                    {
                        __instance.m_DegradeOnUse.m_DegradeHP *= Settings.settings.bedrollDecayOnUse;
                    }
                    //MelonLogger.Msg("bedrollDecay:Custom");
                    //MelonLogger.Msg($"[SETTING] : bedrollDecayDaily:{Settings.settings.bedrollDecayDaily}, bedrollDecayOnUse:{Settings.settings.bedrollDecayOnUse}");
                    //MelonLogger.Msg($"[ACTUAL]  : bedrollDecayDaily:{__instance.m_GearItemData.m_DailyHPDecay}, bedrollDecayOnUse:{__instance.m_DegradeOnUse.m_DegradeHP}");
                }
            }
        }
    }
}
