using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsMode;

[HotSwappable]
public class Settings : ModSettings
{
    internal static bool _printDevMessages = false;
    internal static int _tacticalPauseTicks = 125;
    internal static HashSet<string> _alwaysPauseJobs = [
        nameof(JobDefOf.Arrest),
        nameof(JobDefOf.BuildRoof),
        nameof(JobDefOf.Capture),
        nameof(JobDefOf.CarryToCryptosleepCasket),
        nameof(JobDefOf.Clean),
        nameof(JobDefOf.CutPlant),
        nameof(JobDefOf.CutPlantDesignated),
        nameof(JobDefOf.Deconstruct),
        nameof(JobDefOf.DeliverFood),
        nameof(JobDefOf.DoBill),
        nameof(JobDefOf.DropEquipment),
        nameof(JobDefOf.EnterCryptosleepCasket),
        nameof(JobDefOf.EnterTransporter),
        nameof(JobDefOf.Equip),
        nameof(JobDefOf.EscortPrisonerToBed),
        nameof(JobDefOf.ExtinguishSelf),
        nameof(JobDefOf.FeedPatient),
        nameof(JobDefOf.FillFermentingBarrel),
        nameof(JobDefOf.FinishFrame),
        nameof(JobDefOf.FixBrokenDownBuilding),
        nameof(JobDefOf.Flick),
        nameof(JobDefOf.Goto),
        nameof(JobDefOf.Harvest),
        nameof(JobDefOf.HarvestDesignated),
        nameof(JobDefOf.HaulCorpseToPublicPlace),
        nameof(JobDefOf.HaulToCell),
        nameof(JobDefOf.HaulToContainer),
        nameof(JobDefOf.HaulToTransporter),
        nameof(JobDefOf.Hunt),
        nameof(JobDefOf.Ingest),
        nameof(JobDefOf.Maintain),
        nameof(JobDefOf.ManTurret),
        nameof(JobDefOf.Mine),
        nameof(JobDefOf.Open),
        nameof(JobDefOf.OperateDeepDrill),
        nameof(JobDefOf.PlaceNoCostFrame),
        nameof(JobDefOf.OperateScanner),
        nameof(JobDefOf.PrisonerExecution),
        nameof(JobDefOf.RearmTurret),
        nameof(JobDefOf.RearmTurretAtomic),
        nameof(JobDefOf.Refuel),
        nameof(JobDefOf.RefuelAtomic),
        nameof(JobDefOf.ReleasePrisoner),
        nameof(JobDefOf.RemoveApparel),
        nameof(JobDefOf.RemoveFloor),
        nameof(JobDefOf.RemoveRoof),
        nameof(JobDefOf.Repair),
        nameof(JobDefOf.Rescue),
        nameof(JobDefOf.Shear),
        nameof(JobDefOf.Slaughter),
        nameof(JobDefOf.SmoothFloor),
        nameof(JobDefOf.SmoothWall),
        nameof(JobDefOf.Sow),
        nameof(JobDefOf.Strip),
        nameof(JobDefOf.TakeBeerOutOfFermentingBarrel),
        nameof(JobDefOf.TakeInventory),
        nameof(JobDefOf.TakeToBedToOperate),
        nameof(JobDefOf.TakeWoundedPrisonerToBed),
        nameof(JobDefOf.Tame),
        nameof(JobDefOf.TendPatient),
        nameof(JobDefOf.TradeWithPawn),
        nameof(JobDefOf.Train),
        nameof(JobDefOf.Uninstall),
        nameof(JobDefOf.UseNeurotrainer),
        nameof(JobDefOf.VisitSickPawn),
        nameof(JobDefOf.Wear)
    ];

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref _printDevMessages, "printDevMessages", false);
        Scribe_Values.Look(ref _tacticalPauseTicks, "tacticalPauseTicks", 125);
        Scribe_Collections.Look(ref _alwaysPauseJobs, "alwaysPauseJobs", LookMode.Value);
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

        listingStandard.Label("TM.TimeToPauseAfter".Translate(_tacticalPauseTicks.ToStringTicksToPeriod(allowSeconds: false)));
        _tacticalPauseTicks = (int)listingStandard.Slider(_tacticalPauseTicks, 60, 2500);
        //listingStandard.TextFieldNumeric(ref _tacticalPauseTicks, ref _tacticalPauseTicksBuffer);

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
        foreach (var (name, jobDef) in JobDefHelper.AllJobs.OrderByDescending(j => j.Value.modContentPack.IsOfficialMod))
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
