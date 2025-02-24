using Il2CppTLD.IntBackedUnit;

namespace BedrollTweaker
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal class GEAR_BedRoll_Down
    {
        private static void Postfix(GearItem __instance)
        {
            if (Utilities.NormalizeName(__instance.name) == "GEAR_BedRoll_Down")
            {
                if (Settings.settings.tweakBedroll == Choice.Custom)
                {
                    __instance.m_Bed.m_WarmthBonusCelsius = Settings.settings.bedrollDownWarmth;
                    __instance.WeightKG = ItemWeight.FromKilograms(Settings.settings.bedrollDownWeight);
                    //MelonLogger.Msg("tweakBedroll:Custom");
                    //MelonLogger.Msg($"[SETTING] : bedrollDownWarmth:{Settings.settings.bedrollDownWarmth}, bedrollDownWeight:{Settings.settings.bedrollDownWeight}");
                    //MelonLogger.Msg($"[ACTUAL]  : bedrollDownWarmth:{__instance.m_Bed.m_WarmthBonusCelsius}, bedrollDownWeight:{__instance.WeightKG}");
                }

                if (Settings.settings.bedrollDownDecay == Choice.Custom)
                {
                    __instance.m_GearItemData.m_DailyHPDecay = Settings.settings.bedrollDownDecayDaily;
                    if (__instance.m_DegradeOnUse)
                    {
                        __instance.m_DegradeOnUse.m_DegradeHP *= Settings.settings.bedrollDownDecayOnUse;
                    }
                    //MelonLogger.Msg("bedrollDownDecay:Custom");
                    //MelonLogger.Msg($"[SETTING] : bedrollDownDecayDaily:{Settings.settings.bedrollDownDecayDaily}, bedrollDownDecayOnUse:{Settings.settings.bedrollDownDecayOnUse}");
                    //MelonLogger.Msg($"[ACTUAL]  : bedrollDownDecayDaily:{__instance.m_GearItemData.m_DailyHPDecay}, bedrollDownDecayOnUse:{__instance.m_DegradeOnUse.m_DegradeHP}");
                }
            }
        }
    }
}
