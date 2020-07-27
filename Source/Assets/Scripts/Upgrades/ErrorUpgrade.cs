using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorUpgrade : UpgradeBase
{
    private IntSO errors;

    public ErrorUpgrade(string upgradeName, int level, int maxLevel, IntSO errors) : base(upgradeName, level, maxLevel)
    {
        this.errors = errors;
    }

    public override bool Upgrade()
    {
        errors.Value++;
        return false;
    }
}
