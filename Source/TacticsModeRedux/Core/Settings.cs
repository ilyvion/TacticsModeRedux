using System.Linq;
using UnityEngine;

namespace TacticsModeRedux;

[HotSwappable]
public partial class Settings : ModSettings
{
    internal static bool _printDevMessages = false;
    internal static int _tacticalPauseTicks = 125;
    internal static bool _moveCameraOnPause = true;
    internal static bool _showMessageOnPause = true;

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref _printDevMessages, "printDevMessages", false);
        Scribe_Values.Look(ref _tacticalPauseTicks, "tacticalPauseTicks", 125);
        Scribe_Collections.Look(ref _alwaysPauseJobs, "alwaysPauseJobs", LookMode.Value);
        Scribe_Values.Look(ref _moveCameraOnPause, "moveCameraOnPause", true);
        Scribe_Values.Look(ref _showMessageOnPause, "showMessageOnPause", true);
    }

    private static Vector2 scrollPosition;
    private static Rect scrollRect;
    private static string searchText = "";

    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new();
        listingStandard.Begin(inRect);

        if (Prefs.DevMode)
        {
            listingStandard.CheckboxLabeled("DEV: Print dev log messages (rather verbose)", ref _printDevMessages);
            listingStandard.Gap(4);
        }

        listingStandard.CheckboxLabeled(
            "TM.MoveCameraAndSelectOnPauseLabel".Translate(),
            ref _moveCameraOnPause,
            "TM.MoveCameraAndSelectOnPauseTooltip".Translate());
        listingStandard.Gap(4);

        listingStandard.CheckboxLabeled(
            "TM.ShowMessageOnPauseLabel".Translate(),
            ref _showMessageOnPause,
            "TM.ShowMessageOnPauseTooltip".Translate());
        listingStandard.Gap(4);

        listingStandard.Label(_tacticalPauseTicks > 124
            ? "TM.TimeToPauseAfter".Translate(_tacticalPauseTicks.ToStringTicksToPeriod(allowSeconds: false))
            : "TM.NeverPauseAfterTime".Translate());
        _tacticalPauseTicks = (int)listingStandard.Slider(_tacticalPauseTicks, 124, 2500);

        Text.Font = GameFont.Medium;
        listingStandard.Label("TM.JobsToAlwaysPauseAfter".Translate());

        Text.Font = GameFont.Small;
        searchText = listingStandard.TextEntry(searchText);

        listingStandard.End();

        var height = listingStandard.CurHeight;

        var listingRect = inRect;
        listingRect.yMin += height;

        scrollRect.width = listingRect.width - 20f;
        Widgets.BeginScrollView(listingRect, ref scrollPosition, scrollRect);
        Rect listRect = scrollRect;
        listingStandard = new()
        {
            verticalSpacing = 4f
        };
        listingStandard.Begin(listRect);

        bool searchOn = searchText.Length > 0;
        Rect messageRect = new(scrollRect);
        messageRect.width -= 16f;

        scrollRect.height = 0f;
        foreach (var (name, jobDef) in JobDefHelper.AllJobs.OrderByDescending(j => j.Value.modContentPack?.IsOfficialMod ?? false))
        {
            string labelText = $"{name} - {jobDef.reportString}";

            if (searchOn && !labelText.ToLower().Contains(searchText.ToLower())) continue;
            labelText = Text.ClampTextWithEllipsis(messageRect, labelText);

            scrollRect.height += 26.1f;

            bool isCurrentlyEnabled = _alwaysPauseJobs.Contains(jobDef.defName);
            bool shouldBeEnabled = isCurrentlyEnabled;
            listingStandard.CheckboxLabeled(labelText, ref shouldBeEnabled);
            if (shouldBeEnabled && !isCurrentlyEnabled)
            {
                _alwaysPauseJobs.Add(jobDef.defName);
            }
            else if (!shouldBeEnabled && isCurrentlyEnabled)
            {
                _alwaysPauseJobs.Remove(jobDef.defName);
            }
        }

        listingStandard.End();
        Widgets.EndScrollView();
    }
}
