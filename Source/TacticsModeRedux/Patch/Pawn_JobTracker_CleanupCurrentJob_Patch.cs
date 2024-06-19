using Verse.AI;

namespace TacticsModeRedux.Patch;

[HarmonyPatch(typeof(Pawn_JobTracker), "CleanupCurrentJob")]
internal class Verse_AI_Pawn_JobTracker_CleanupCurrentJob
{
    internal static Func<Pawn, bool> NotDoingAchtungForcedWork = (_) => true;
    static void Prefix(Pawn_JobTracker __instance)
    {
        Pawn p = Traverse.Create(__instance).Field<Pawn>("pawn").Value;

        if (__instance.IsCurrentJobPlayerInterruptible()
            && __instance.jobQueue.Count == 0
            && (__instance.curJob == null
                || Settings._alwaysPauseJobs.Contains(__instance.curJob.def.defName)
                || TacticsModeGameComponent.Current.HasTimeToPauseExpired(p))
            && NotDoingAchtungForcedWork(p))
        {
            TacticsModeGameComponent.Current.TryDoTacticalAction(p, __instance.curJob);
        }
    }
}
