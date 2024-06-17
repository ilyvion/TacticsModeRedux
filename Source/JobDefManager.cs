using System.Collections.Generic;
using System.Linq;

namespace TacticsMode;

internal static class JobDefHelper
{
    private static Dictionary<string, JobDef>? _allJobs;
    public static Dictionary<string, JobDef> AllJobs
    {
        get
        {
            _allJobs ??= DefDatabase<JobDef>.AllDefs.OrderBy(j => j.defName).ToDictionary(j => j.defName);
            return _allJobs;
        }
    }
}
