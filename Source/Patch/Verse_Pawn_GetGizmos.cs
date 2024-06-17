using System.Collections.Generic;

namespace TacticsMode.Patch;

[HarmonyPatch(typeof(Pawn), "GetGizmos")]
class Verse_Pawn_GetGizmos
{
    static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> gizmos, Pawn __instance)
    {
        foreach (Gizmo gizmo in gizmos)
            yield return gizmo;
        var tactical = new Command_Toggle
        {
            icon = ContentFinder<UnityEngine.Texture2D>.Get("Buttons/Pawn", true),
            defaultLabel = "Tactics",
            defaultDesc = "Toggle tactics mode for this colonist. The game will pause and center on the colonist when they finish a job, allowing you to micromanage them.",
            isActive = () => TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance),
            toggleAction = delegate
            {
                TacticsModeGameComponent.Current.SetTacticsMode(__instance, !TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance));
            },
            hotKey = null
        };
        if (TacticsModeGameComponent.CanEverBeInTacticsMode(__instance))
            yield return tactical;
    }
}
