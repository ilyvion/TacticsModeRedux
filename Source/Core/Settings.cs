using UnityEngine;

namespace TacticsMode;

public class Settings : ModSettings
{
    internal static bool _printDevMessages = false;

    public override void ExposeData()
    {
        base.ExposeData();

        // Meta
        Scribe_Values.Look(ref _printDevMessages, "printDevMessages", false);
    }


    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new();
        listingStandard.Begin(inRect);

        if (Prefs.DevMode)
        {
            listingStandard.CheckboxLabeled("DEV: Print dev log messages (rather verbose)", ref _printDevMessages);
            listingStandard.Gap(4);
        }

        listingStandard.End();
    }
}
