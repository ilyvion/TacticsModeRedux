using System.Collections.Generic;
using System.Linq;

using RWKeyBindingDefOf = RimWorld.KeyBindingDefOf;

namespace TacticsModeRedux.Patch;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.GetGizmos))]
class Verse_Pawn_GetGizmos
{
    static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> gizmos, Pawn __instance)
    {
        if (!TacticsModeGameComponent.CanEverBeInTacticsMode(__instance))
            return gizmos;

        return GetGizmos();

        IEnumerable<Gizmo> GetGizmos()
        {
            var tacticsModeGizmo = new Command_Toggle
            {
                icon = ContentFinder<UnityEngine.Texture2D>.Get("Buttons/Pawn", true),
                defaultLabel = "TM.ToggleTacticsModeLabel".Translate(),
                defaultDesc = "TM.ToggleTacticsModeDesc".Translate(),
                isActive = () => TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance),
                toggleAction = () =>
                {
                    TacticsModeGameComponent.Current.SetTacticsMode(__instance, !TacticsModeGameComponent.Current.HasTacticsModeToggledOn(__instance));
                },
                hotKey = KeyBindingDefOf.TM_Command_ToggleTacticsMode
            };

            bool alreadyReturned = false;
            foreach (var gizmo in gizmos)
            {
                yield return gizmo;

                if (gizmo is Command_Toggle toggle && toggle.icon == TexCommand.Draft && toggle.hotKey == RWKeyBindingDefOf.Command_ColonistDraft)
                {
                    alreadyReturned = true;
                    yield return tacticsModeGizmo;
                }
            }

            if (!alreadyReturned)
            {
                yield return tacticsModeGizmo;
            }
        }
    }
}
