using Il2CppTLD.IntBackedUnit;

namespace BedrollTweaker
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal class GEAR_MakeshiftBedroll
    {
        private static void Postfix(GearItem __instance)
        {
            if (Utilities.NormalizeName(__instance.name) == "GEAR_MakeshiftBedroll")
            {
                if (Settings.settings.tweakBedroll == Choice.Custom)
                {
                    __instance.m_Bed.m_WarmthBonusCelsius = Settings.settings.makeshiftBedrollWarmth;
                    __instance.WeightKG = ItemWeight.FromKilograms(Settings.settings.makeshiftBedrollWeight);
                    //MelonLogger.Msg("tweakBedroll:Custom");
                    //MelonLogger.Msg($"[SETTING] : makeshiftBedrollWarmth:{Settings.settings.makeshiftBedrollWarmth}, makeshiftBedrollWeight:{Settings.settings.makeshiftBedrollWeight}");
                    //MelonLogger.Msg($"[ACTUAL]  : makeshiftBedrollWarmth:{__instance.m_Bed.m_WarmthBonusCelsius}, makeshiftBedrollWeight:{__instance.WeightKG}");
                }

                if (Settings.settings.makeshiftBedrollDecay == Choice.Custom)
                {
                    __instance.m_GearItemData.m_DailyHPDecay = Settings.settings.makeshiftBedrollDecayDaily;
                    if (__instance.m_DegradeOnUse)
                    {
                        __instance.m_DegradeOnUse.m_DegradeHP *= Settings.settings.makeshiftBedrollDecayOnUse;
                    }
                    //MelonLogger.Msg("makeshiftBedrollDecay:Custom");
                    //MelonLogger.Msg($"[SETTING] : makeshiftBedrollDecayDaily:{Settings.settings.makeshiftBedrollDecayDaily}, makeshiftBedrollDecayOnUse:{Settings.settings.makeshiftBedrollDecayOnUse}");
                    //MelonLogger.Msg($"[ACTUAL]  : makeshiftBedrollDecayDaily:{__instance.m_GearItemData.m_DailyHPDecay}, makeshiftBedrollDecayOnUse:{__instance.m_DegradeOnUse.m_DegradeHP}");
                }
            }
        }
    }
}
