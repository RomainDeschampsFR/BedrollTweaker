using Il2CppTLD.IntBackedUnit;

namespace BedrollTweaker
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal class GEAR_BearskinBedRoll
    {
        private static void Postfix(GearItem __instance)
        {
            if (Utilities.NormalizeName(__instance.name) == "GEAR_BearSkinBedRoll")
            {
                if (Settings.settings.tweakBearskinBedroll == Choice.Custom)
                {
                    __instance.m_Bed.m_WarmthBonusCelsius = Settings.settings.bearskinBedrollWarmth;
                    __instance.WeightKG = ItemWeight.FromKilograms(Settings.settings.bearskinBedrollWeight);
                    //MelonLogger.Msg("tweakBearskinBedroll:Custom");
                    //MelonLogger.Msg($"[SETTING] : bearskinBedrollWarmth:{Settings.settings.bearskinBedrollWarmth},  bearskinBedrollWeight:{Settings.settings.bearskinBedrollWeight}");
                    //MelonLogger.Msg($"[ACTUAL]  : bearskinBedrollWarmth:{__instance.m_Bed.m_WarmthBonusCelsius},    bearskinBedrollWeight:{__instance.WeightKG}");
                }

                if (Settings.settings.bearskinBedrollDecay == Choice.Custom)
                {
                    __instance.m_GearItemData.m_DailyHPDecay = Settings.settings.bearskinBedrollDecayDaily;
                    if (__instance.m_DegradeOnUse)
                    {
                        __instance.m_DegradeOnUse.m_DegradeHP *= Settings.settings.bearskinBedrollDecayOnUse;
                    }
                    //MelonLogger.Msg("bearskinBedrollDecay:Custom");
                    //MelonLogger.Msg($"[SETTING] : bearskinBedrollDecayDaily:{Settings.settings.bearskinBedrollDecayDaily},  bearskinBedrollDecayOnUse:{Settings.settings.bearskinBedrollDecayOnUse}");
                    //MelonLogger.Msg($"[ACTUAL]  : bearskinBedrollDecayDaily:{__instance.m_GearItemData.m_DailyHPDecay},     bearskinBedrollDecayOnUse:{__instance.m_DegradeOnUse.m_DegradeHP}");
                }
            }
        }
    }
}
