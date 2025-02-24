namespace BedrollTweaker
{
    [HarmonyPatch(typeof(Bed), nameof(Bed.GetWarmthBonusCelsius))]
    internal class BedrollWarmthStackerBonus
    {
        private static void Postfix(Bed __instance, ref float __result)
        {
            List<float> totalbedrolls   = new();
            List<float> partial         = new();

            float bedrollStack          = 0f;
            float num                   = __instance.m_WarmthBonusCelsius;

            if (Settings.settings.modFunction && Settings.settings.bedrollsStack)
            {

                // Obtain all the bedrolls
                foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
                {
                    GearItem gi = eachItem.GetComponent<GearItem>();
                    if (gi == null) continue;

                    Bed bed = gi.m_Bed;
                    if (bed == null) continue;

                    if (bed.m_Bedroll)
                    {
                        totalbedrolls.Add(bed.m_WarmthBonusCelsius * bed.m_Bedroll.GetNormalizedCondition());
                        bedrollStack += (bed.m_WarmthBonusCelsius * bed.m_Bedroll.GetNormalizedCondition());
                    }
                }

                if (Settings.settings.maxBedrolls || Settings.settings.diminishingBonus || Settings.settings.partialBonus)
                {
                    totalbedrolls.Sort();
                    totalbedrolls.Reverse();

                    bedrollStack = 0f;

                    if (Settings.settings.maxBedrolls) totalbedrolls = totalbedrolls.Take(Settings.settings.maxBedrollsNumber).ToList();
                    if (!Settings.settings.diminishingBonus && !Settings.settings.partialBonus)
                    {
                        foreach (float value in  totalbedrolls) bedrollStack += value;
                    }
                    if (Settings.settings.partialBonus)
                    {
                        foreach (float value in totalbedrolls)
                        {
                            partial.Add(value * Settings.settings.partialRate);
                            bedrollStack += (value * Settings.settings.partialRate);
                        }
                        totalbedrolls = partial;
                    }
                    if (Settings.settings.diminishingBonus)
                    {
                        bedrollStack = 0f;
                        float mult = 1 - Settings.settings.diminishingRate;
                        foreach (float value in totalbedrolls)
                        {
                            bedrollStack += (value * mult);
                            mult *= mult;
                        }
                    }
                }
                if (Settings.settings.capWarmthBonus) bedrollStack = Math.Min(bedrollStack, Settings.settings.warmthBonusCap);

                __result += bedrollStack;
            }
        }
    }
}