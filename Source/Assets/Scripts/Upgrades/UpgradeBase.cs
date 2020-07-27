using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UpgradeBase
{
    private string upgradeName;
    protected int level;
    protected int maxLevel;

    public string UpgradeName { get => upgradeName; set => upgradeName = value; }

    public UpgradeBase(string upgradeName, int level, int maxLevel)
    {
        this.upgradeName = upgradeName;
        this.level = level;
        this.maxLevel = maxLevel;
    }

    abstract public bool Upgrade();
}
