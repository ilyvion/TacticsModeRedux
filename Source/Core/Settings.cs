using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsModeRedux;

[HotSwappable]
public class Settings : ModSettings
{
    internal static bool _printDevMessages = false;
    internal static int _tacticalPauseTicks = 125;
    internal static HashSet<string> _alwaysPauseJobs = [
        nameof(JobDefOf.Arrest),
        nameof(JobDefOf.AttackMelee),
        nameof(JobDefOf.AttackStatic),
        nameof(JobDefOf.BuildRoof),
        nameof(JobDefOf.Capture),
        nameof(JobDefOf.CarryDownedPawnDrafted),
        nameof(JobDefOf.CarryDownedPawnToExit),
        nameof(JobDefOf.CarryDownedPawnToPortal),
        nameof(JobDefOf.CarryToCryptosleepCasket),
        nameof(JobDefOf.CarryToCryptosleepCasketDrafted),
        nameof(JobDefOf.CarryToPrisonerBedDrafted),
        nameof(JobDefOf.Clean),
        nameof(JobDefOf.ClearSnow),
        nameof(JobDefOf.CutPlant),
        nameof(JobDefOf.CutPlantDesignated),
        nameof(JobDefOf.Deconstruct),
        nameof(JobDefOf.DeliverFood),
        nameof(JobDefOf.DeliverToBed),
        nameof(JobDefOf.DeliverToCell),
        nameof(JobDefOf.DismissTrader),
        nameof(JobDefOf.DoBill),
        nameof(JobDefOf.DropEquipment),
        nameof(JobDefOf.EmptyThingContainer),
        nameof(JobDefOf.EnterCryptosleepCasket),
        nameof(JobDefOf.EnterPortal),
        nameof(JobDefOf.EnterTransporter),
        nameof(JobDefOf.Equip),
        nameof(JobDefOf.EscortPrisonerToBed),
        nameof(JobDefOf.ExtinguishFiresNearby),
        nameof(JobDefOf.ExtinguishSelf),
        nameof(JobDefOf.ExtractSkull),
        nameof(JobDefOf.ExtractTree),
        nameof(JobDefOf.FeedPatient),
        nameof(JobDefOf.FillFermentingBarrel),
        nameof(JobDefOf.FinishFrame),
        nameof(JobDefOf.FixBrokenDownBuilding),
        nameof(JobDefOf.Flee),
        nameof(JobDefOf.FleeAndCower),
        nameof(JobDefOf.Flick),
        nameof(JobDefOf.Goto),
        nameof(JobDefOf.GuiltyColonistExecution),
        nameof(JobDefOf.Harvest),
        nameof(JobDefOf.HarvestDesignated),
        nameof(JobDefOf.HaulCorpseToPublicPlace),
        nameof(JobDefOf.HaulToCell),
        nameof(JobDefOf.HaulToContainer),
        nameof(JobDefOf.HaulToPortal),
        nameof(JobDefOf.HaulToTransporter),
        nameof(JobDefOf.Hunt),
        nameof(JobDefOf.Ingest),
        nameof(JobDefOf.InteractThing),
        nameof(JobDefOf.Maintain),
        nameof(JobDefOf.ManTurret),
        nameof(JobDefOf.Milk),
        nameof(JobDefOf.Mine),
        nameof(JobDefOf.Open),
        nameof(JobDefOf.OperateDeepDrill),
        nameof(JobDefOf.OperateScanner),
        nameof(JobDefOf.PaintBuilding),
        nameof(JobDefOf.PaintFloor),
        nameof(JobDefOf.PickupToHold),
        nameof(JobDefOf.PlaceNoCostFrame),
        nameof(JobDefOf.PlantSeed),
        nameof(JobDefOf.PrisonerAttemptRecruit),
        nameof(JobDefOf.PrisonerExecution),
        nameof(JobDefOf.Reading),
        nameof(JobDefOf.RearmTurret),
        nameof(JobDefOf.RearmTurretAtomic),
        nameof(JobDefOf.Refuel),
        nameof(JobDefOf.RefuelAtomic),
        nameof(JobDefOf.ReleaseAnimalToWild),
        nameof(JobDefOf.ReleasePrisoner),
        nameof(JobDefOf.Reload),
        nameof(JobDefOf.RemoveApparel),
        nameof(JobDefOf.RemoveFloor),
        nameof(JobDefOf.RemovePaintBuilding),
        nameof(JobDefOf.RemovePaintFloor),
        nameof(JobDefOf.RemoveRoof),
        nameof(JobDefOf.Repair),
        nameof(JobDefOf.Replant),
        nameof(JobDefOf.Rescue),
        nameof(JobDefOf.Resurrect),
        nameof(JobDefOf.RopeRoamerToHitchingPost),
        nameof(JobDefOf.RopeRoamerToUnenclosedPen),
        nameof(JobDefOf.RopeToPen),
        nameof(JobDefOf.Shear),
        nameof(JobDefOf.Slaughter),
        nameof(JobDefOf.SmoothFloor),
        nameof(JobDefOf.SmoothWall),
        nameof(JobDefOf.Sow),
        nameof(JobDefOf.Strip),
        nameof(JobDefOf.TakeBeerOutOfFermentingBarrel),
        nameof(JobDefOf.TakeCountToInventory),
        nameof(JobDefOf.TakeDownedPawnToBedDrafted),
        nameof(JobDefOf.TakeFromOtherInventory),
        nameof(JobDefOf.TakeInventory),
        nameof(JobDefOf.TakeToBedToOperate),
        nameof(JobDefOf.TakeWoundedPrisonerToBed),
        nameof(JobDefOf.Tame),
        nameof(JobDefOf.TendPatient),
        nameof(JobDefOf.TradeWithPawn),
        nameof(JobDefOf.Train),
        "TriggerObject",
        nameof(JobDefOf.Uninstall),
        nameof(JobDefOf.UnloadInventory),
        nameof(JobDefOf.UnloadYourInventory),
        nameof(JobDefOf.Unrope),
        "UseArtifact",
        "UseItem",
        nameof(JobDefOf.UseNeurotrainer),
        nameof(JobDefOf.VisitSickPawn),
        nameof(JobDefOf.Wear),
        // Royalty
        nameof(JobDefOf.AcceptRole),
        nameof(JobDefOf.ApplyTechprint),
        // Ideology
        nameof(JobDefOf.ActivateArchonexusCore),
        nameof(JobDefOf.Blind),
        nameof(JobDefOf.ChangeTreeMode),
        nameof(JobDefOf.DeliverToAltar),
        nameof(JobDefOf.DyeHair),
        nameof(JobDefOf.EatAtCannibalPlatter),
        nameof(JobDefOf.ExtractRelic),
        nameof(JobDefOf.ExtractToInventory),
        nameof(JobDefOf.GetNeuralSupercharge),
        nameof(JobDefOf.GiveSpeech),
        nameof(JobDefOf.Hack),
        nameof(JobDefOf.InstallRelic),
        nameof(JobDefOf.LinkPsylinkable),
        nameof(JobDefOf.Meditate),
        nameof(JobDefOf.MeditatePray),
        nameof(JobDefOf.PrisonerConvert),
        nameof(JobDefOf.PrisonerEnslave),
        nameof(JobDefOf.PrisonerReduceWill),
        nameof(JobDefOf.PruneGauranlenTree),
        nameof(JobDefOf.RecolorApparel),
        nameof(JobDefOf.Reign),
        nameof(JobDefOf.Sacrifice),
        nameof(JobDefOf.Scarify),
        nameof(JobDefOf.SlaveEmancipation),
        nameof(JobDefOf.SlaveExecution),
        nameof(JobDefOf.SlaveSuppress),
        nameof(JobDefOf.UseStylingStation),
        nameof(JobDefOf.UseStylingStationAutomatic),
        // Biotech
        nameof(JobDefOf.AbsorbXenogerm),
        nameof(JobDefOf.BabyPlay),
        nameof(JobDefOf.BabySuckle),
        nameof(JobDefOf.BottleFeedBaby),
        nameof(JobDefOf.Breastfeed),
        nameof(JobDefOf.BreastfeedCarryToMom),
        nameof(JobDefOf.BringBabyToSafety),
        nameof(JobDefOf.BringBabyToSafetyUnforced),
        nameof(JobDefOf.CarryGenepackToContainer),
        nameof(JobDefOf.CarryToBiosculpterPod),
        nameof(JobDefOf.CarryToBuilding),
        nameof(JobDefOf.CarryToMomAfterBirth),
        nameof(JobDefOf.ClearPollution),
        nameof(JobDefOf.ControlMech),
        nameof(JobDefOf.CreateXenogerm),
        nameof(JobDefOf.Deathrest),
        nameof(JobDefOf.DisassembleMech),
        nameof(JobDefOf.EmptyWasteContainer),
        nameof(JobDefOf.EnterBiosculpterPod),
        nameof(JobDefOf.EnterBuilding),
        nameof(JobDefOf.FertilizeOvum),
        nameof(JobDefOf.GetReimplanted),
        nameof(JobDefOf.HaulMechToCharger),
        nameof(JobDefOf.HaulToAtomizer),
        "InstallMechlink",
        nameof(JobDefOf.Lessongiving),
        nameof(JobDefOf.Lessontaking),
        "PlayStatic",
        "PlayToys",
        "PlayWalking",
        nameof(JobDefOf.PrisonerBloodfeed),
        "ReadDatacore",
        nameof(JobDefOf.ReleaseMechs),
        nameof(JobDefOf.RemoveMechlink),
        nameof(JobDefOf.RepairMech),
        "RepairMechRemote",
        // Anomaly
        nameof(JobDefOf.ActivateMonolith),
        nameof(JobDefOf.ActivitySuppression),
        nameof(JobDefOf.AnalyzeItem),
        nameof(JobDefOf.BuildCubeSculpture),
        nameof(JobDefOf.CarryToEntityHolder),
        nameof(JobDefOf.CarryToEntityHolderAlreadyHolding),
        nameof(JobDefOf.ExecuteEntity),
        nameof(JobDefOf.ExtractBioferrite),
        nameof(JobDefOf.FillIn),
        nameof(JobDefOf.FleeAndCowerShort),
        nameof(JobDefOf.InvestigateMonolith),
        nameof(JobDefOf.PrisonerInterrogateIdentity),
        nameof(JobDefOf.ReleaseEntity),
        nameof(JobDefOf.TakeBioferriteOutOfHarvester),
        nameof(JobDefOf.TalkCreepJoiner),
        nameof(JobDefOf.TendEntity),
        nameof(JobDefOf.TransferBetweenEntityHolders),
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
