using AchtungMod;
using TacticsModeRedux.Patch;

namespace TacticsModeRedux.Achtung;

[StaticConstructorOnStartup]
class AddAchtungFeatures
{
    static AddAchtungFeatures()
    {
        TacticsModeReduxMod.Message("\"Achtung\" interop loaded successfully!");

        Settings.hasAchtung = true;
        Verse_AI_Pawn_JobTracker_CleanupCurrentJob.NotDoingAchtungForcedWork = (pawn) =>
        {
            bool pawnHasForcedJob = ForcedWork.Instance.HasForcedJob(pawn);
            return !pawnHasForcedJob;
        };
    }
}
