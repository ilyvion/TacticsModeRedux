using System.Collections.Generic;
using System.Linq;

namespace TacticsModeRedux.Patch;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.GetGizmos))]
class Verse_Pawn_GetGizmos
{
    static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> gizmos, Pawn __instance)
    {
        if (!TacticsModeGameComponent.CanEverBeInTacticsMode(__instance))
            return gizmos;

        return (gizmos ?? []).Concat(new Command_Toggle
        {
            icon = ContentFinder<UnityEngine.Texture2D>.Get("Buttons/Pawn", true),
            defaultLabel = "TM.ToggleTacticsModeLabel".Translate(),
            defaultDesc = "TM.ToggleTacticsModeDesc".Translate(),
            isActive = () => TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance),
            toggleAction = () =>
            {
                TacticsModeGameComponent.Current.SetTacticsMode(__instance, !TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance));
            },
            hotKey = null
        });
    }
}
