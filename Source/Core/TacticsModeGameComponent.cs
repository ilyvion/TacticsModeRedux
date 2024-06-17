using System.Collections.Generic;
using System.Linq;

using VerseCurrent = Verse.Current;

namespace TacticsMode;

public class TacticsModeGameComponent : GameComponent
{
    public static TacticsModeGameComponent Current => VerseCurrent.Game.GetComponent<TacticsModeGameComponent>();

    private Dictionary<Pawn, bool> _inTacticsMode = [];
    private Dictionary<Pawn, int> _lastActionTick = [];
    private List<Pawn> scribe_inTacticsMode_keys = [];
    private List<bool> scribe_inTacticsMode_values = [];

    public TacticsModeGameComponent(Game game) { }

    public bool LastActionExpired(Pawn p)
    {
        int lastActionTick;
        _lastActionTick.TryGetValue(p, out lastActionTick);
        return Find.TickManager.TicksGame > lastActionTick + 90;
    }

    public static bool CanEverBeInTacticsMode(Pawn p)
    {
        return p.IsColonist;
    }

    public bool HasTacticsModeToggledOn(Pawn p)
    {
        try
        {
            return _inTacticsMode[p];
        }
        catch { }
        return false;
    }

    public void SetTacticsMode(Pawn p, bool v)
    {
        if (!CanEverBeInTacticsMode(p))
            Log.Warning("Tactical mode set on non-player-controlled pawn -- this will have no effect.");
        _inTacticsMode[p] = v;
    }

    public bool IsInTacticsMode(Pawn p)
    {
        return CanEverBeInTacticsMode(p) && p.IsColonistPlayerControlled && HasTacticsModeToggledOn(p);
    }
    public override void ExposeData()
    {
        if (Scribe.mode == LoadSaveMode.Saving)
            Clean();
        Scribe_Collections.Look(ref _inTacticsMode, "tacticalMode", LookMode.Reference, LookMode.Value,
            ref scribe_inTacticsMode_keys, ref scribe_inTacticsMode_values);
    }
    public void Clean()
    {
        var destroyed_pawns = new List<Pawn>(_inTacticsMode.Keys.Where(p => p.Destroyed));
        foreach (var p in destroyed_pawns)
            _inTacticsMode.Remove(p);
    }

    public void TryDoTacticalAction(Pawn p)
    {
        if (IsInTacticsMode(p))
        {
            CameraJumper.TryJumpAndSelect(p);
            Find.TickManager.Pause();
            _lastActionTick[p] = Find.TickManager.TicksGame;
        }
    }
}
