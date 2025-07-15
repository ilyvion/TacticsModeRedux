using System.Linq;
using UnityEngine;

namespace TacticsModeRedux;

[HotSwappable]
public partial class Settings : ModSettings
{
    internal static bool hasAchtung = false;

    internal static bool _printDevMessages = false;
    internal static int _tacticalPauseTicks = 125;
    internal static bool _moveCameraOnPause = true;
    internal static bool _showMessageOnPause = true;
    internal static bool _achtungForcedWorkPreventsPause = true;

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref _printDevMessages, "printDevMessages", false);
        Scribe_Values.Look(ref _tacticalPauseTicks, "tacticalPauseTicks", 125);
        Scribe_Collections.Look(ref _alwaysPauseJobs, "alwaysPauseJobs", LookMode.Value);
        Scribe_Values.Look(ref _moveCameraOnPause, "moveCameraOnPause", true);
        Scribe_Values.Look(ref _showMessageOnPause, "showMessageOnPause", true);
        Scribe_Values.Look(ref _achtungForcedWorkPreventsPause, "achtungForcedWorkPreventsPause", true);
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

        if (hasAchtung)
        {
            listingStandard.CheckboxLabeled(
                "TM.AchtungForcedWorkPreventsPauseLabel".Translate(),
                ref _achtungForcedWorkPreventsPause);
            listingStandard.Gap(4);
        }

        _ = listingStandard.Label(_tacticalPauseTicks > 124
            ? "TM.TimeToPauseAfter".Translate(_tacticalPauseTicks.ToStringTicksToPeriod(allowSeconds: false))
            : "TM.NeverPauseAfterTime".Translate());
        _tacticalPauseTicks = (int)listingStandard.Slider(_tacticalPauseTicks, 124, 2500);

        Text.Font = GameFont.Medium;
        _ = listingStandard.Label("TM.JobsToAlwaysPauseAfter".Translate());

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
        string? currentModContentPackName = null;
        foreach (var (name, jobDef) in JobDefHelper.AllJobs
            .Where(j =>
            {
                return !searchOn
                    || j.Key.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || j.Value.reportString.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0;
            })
            .OrderByDescending(j => j.Value.modContentPack?.IsOfficialMod ?? false)
            .ThenBy(j =>
            {
                return j.Value.modContentPack?.IsOfficialMod ?? false
                    ? (j.Value.modContentPack?.Name) switch
                    {
                        "Core" => "A",
                        "Royalty" => "B",
                        "Ideology" => "C",
                        "Biotech" => "D",
                        "Anomaly" => "E",
                        "Odyssey" => "F",
                        _ => "G",
                    }
                    : j.Value.modContentPack?.Name ?? "Programmatically Added";
            })
            )
        {
            if (currentModContentPackName != (jobDef.modContentPack?.Name ?? "Programmatically Added"))
            {
                if (currentModContentPackName != null)
                {
                    listingStandard.Gap();
                    scrollRect.height += 30f;
                }
                currentModContentPackName = jobDef.modContentPack?.Name ?? "Programmatically Added";
                _ = listingStandard.Label($"<b>{currentModContentPackName}</b>");
                scrollRect.height += 30f;
            }

            string labelText = $"{name} - {jobDef.reportString}";

            labelText = Text.ClampTextWithEllipsis(messageRect, labelText);

            bool isCurrentlyEnabled = _alwaysPauseJobs.Contains(jobDef.defName);
            bool shouldBeEnabled = isCurrentlyEnabled;
            listingStandard.CheckboxLabeled(labelText, ref shouldBeEnabled);
            scrollRect.height += 30f;
            if (shouldBeEnabled && !isCurrentlyEnabled)
            {
                _ = _alwaysPauseJobs.Add(jobDef.defName);
            }
            else if (!shouldBeEnabled && isCurrentlyEnabled)
            {
                _ = _alwaysPauseJobs.Remove(jobDef.defName);
            }
        }

        if (Event.current.type == EventType.Layout)
        {
            scrollRect.height = listingStandard.CurHeight;
        }
        listingStandard.End();
        Widgets.EndScrollView();
    }
}
