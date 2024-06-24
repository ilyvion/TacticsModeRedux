#nullable disable

namespace TacticsModeRedux;

[DefOf]
class KeyBindingDefOf
{
    public static KeyBindingDef TM_Command_ToggleTacticsMode;

    static KeyBindingDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(KeyBindingDefOf));
    }
}
