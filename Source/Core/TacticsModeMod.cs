using System.Reflection;
using UnityEngine;

namespace TacticsModeRedux;

public class TacticsModeReduxMod : Mod
{
#pragma warning disable CS8618 // Set by constructor
    internal static TacticsModeReduxMod instance;
#pragma warning restore CS8618

    public TacticsModeReduxMod(ModContentPack content) : base(content)
    {
        instance = this;
        new Harmony(content.PackageId).PatchAll(Assembly.GetExecutingAssembly());

        GetSettings<Settings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        Settings.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return Content.Name;
    }

    public static void Message(string msg)
    {
        Log.Message("[Tactics Mode Redux] " + msg);
    }

    public static void Dev(string msg)
    {
        if (Prefs.DevMode && Settings._printDevMessages)
        {
            Log.Message("[Tactics Mode Redux][DEV] " + msg);
        }
    }

    public static void Dev(Func<string> produceMsg)
    {
        if (Prefs.DevMode && Settings._printDevMessages)
        {
            Log.Message("[Tactics Mode Redux][DEV] " + produceMsg());
        }
    }

    public static void Warning(string msg)
    {
        Log.Warning("[Tactics Mode Redux] " + msg);
    }

    public static void Error(string msg)
    {
        Log.Error("[Tactics Mode Redux] " + msg);
    }

    public static void Exception(string msg, Exception? e = null)
    {
        Message(msg);
        if (e != null)
        {
            Log.Error(e.ToString());
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class HotSwappableAttribute : Attribute
{
}
