using Verse.AI;

namespace TacticsMode.Patch;

[HarmonyPatch(typeof(Pawn_JobTracker), "CleanupCurrentJob")]
class Verse_AI_Pawn_JobTracker_CleanupCurrentJob
{
    static void Prefix(Pawn_JobTracker __instance)
    {
        Pawn_JobTracker tracker = __instance;
        Pawn p = Traverse.Create(__instance).Field<Pawn>("pawn").Value;

        if (tracker.IsCurrentJobPlayerInterruptible()
            && tracker.jobQueue.Count == 0
            && (tracker.curJob == null
                || JobTypeWhitelist.JobTypeWhitelistHashSet.Contains(tracker.curJob.def)
                || TacticsModeGameComponent.Current.LastActionExpired(p)))
        {
            TacticsModeGameComponent.Current.TryDoTacticalAction(p);
        }
    }
}
