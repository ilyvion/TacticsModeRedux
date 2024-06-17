using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TacticsMode;

public static class JobTypeWhitelist
{
    private static string[] _job_type_whitelist_names = [
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
        //nameof(JobDefOf.TriggerFirefoamPopper),
        nameof(JobDefOf.Uninstall),
        //nameof(JobDefOf.UseArtifact),
        nameof(JobDefOf.UseNeurotrainer),
        nameof(JobDefOf.VisitSickPawn),
        nameof(JobDefOf.Wear)
    ];
    private static JobDef? GetJobDefByName(string name)
    {
        JobDef? def = null;
        try
        {
            def = (JobDef?)AccessTools.Field(typeof(JobDefOf), name).GetValue(null);
            if (def == null) throw new Exception();
        }
        catch
        {
            Log.Error("TacticsMode Can't find JobDefOf for " + name);
        }
        return def;
    }

    private static HashSet<JobDef>? _jobTypeWhitelist = null;
    public static HashSet<JobDef> JobTypeWhitelistHashSet
    {
        get
        {
            _jobTypeWhitelist ??= new HashSet<JobDef>(_job_type_whitelist_names.Select(GetJobDefByName).Where(d => d != null)!);
            return _jobTypeWhitelist;
        }
    }
}
