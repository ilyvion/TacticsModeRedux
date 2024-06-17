using System.Collections.Generic;
using System.Linq;

using VerseCurrent = Verse.Current;

namespace TacticsModeRedux;

public class TacticsModeGameComponent : GameComponent
{
    public static TacticsModeGameComponent Current => VerseCurrent.Game.GetComponent<TacticsModeGameComponent>();

    private HashSet<Pawn> _pawnsInTacticsMode = [];
    private Dictionary<Pawn, int> _lastActionTick = [];

    public TacticsModeGameComponent(Game _) { }

    public bool HasTimeToPauseExpired(Pawn pawn)
    {
        _lastActionTick.TryGetValue(pawn, out int lastActionTick);
        return Find.TickManager.TicksGame > lastActionTick + Settings._tacticalPauseTicks;
    }

    public static bool CanEverBeInTacticsMode(Pawn pawn)
    {
        return pawn.IsColonist;
    }

    public bool HasTacticsModeToggledOn(Pawn pawn)
    {
        return _pawnsInTacticsMode.Contains(pawn);
    }

    public void SetTacticsMode(Pawn pawn, bool putInTacticsMode)
    {
        if (!CanEverBeInTacticsMode(pawn))
            TacticsModeReduxMod.Warning("Tactical mode set on non-player-controlled pawn -- this will have no effect.");
        if (putInTacticsMode)
        {
            _pawnsInTacticsMode.Add(pawn);
        }
        else
        {
            _pawnsInTacticsMode.Remove(pawn);
        }
    }

    public bool IsInTacticsMode(Pawn p)
    {
        return CanEverBeInTacticsMode(p) && p.IsColonistPlayerControlled && HasTacticsModeToggledOn(p);
    }

    public override void ExposeData()
    {
        if (Scribe.mode == LoadSaveMode.Saving)
        {
            RemoveDestroyedPawns();
        }

        Scribe_Collections.Look(ref _pawnsInTacticsMode, "pawnsInTacticsMode", LookMode.Reference);
    }

    public void RemoveDestroyedPawns()
    {
        var destroyedPawns = new List<Pawn>(_pawnsInTacticsMode.Where(p => p.Destroyed));
        foreach (var p in destroyedPawns)
        {
            _pawnsInTacticsMode.Remove(p);
        }
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