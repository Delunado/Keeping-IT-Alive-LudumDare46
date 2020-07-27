using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpgrade : UpgradeBase
{
    private IntSO maxModules;

    public TimeUpgrade(string upgradeName, int level, int maxlevel, IntSO maxModules) : base(upgradeName, level, maxlevel)
    {
        this.maxModules = maxModules;
    }

    public override bool Upgrade()
    {
        maxModules.Value += 4;
        return false;
    }
}
