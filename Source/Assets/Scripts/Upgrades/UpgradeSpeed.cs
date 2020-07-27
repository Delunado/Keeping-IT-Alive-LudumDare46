using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeed : UpgradeBase
{
    FloatSO playerSpeed;

    public UpgradeSpeed(string upgradeName, int level, int maxlevel, FloatSO playerSpeed) : base(upgradeName, level, maxlevel)
    {
        this.playerSpeed = playerSpeed;
    }

    public override bool Upgrade()
    {
        playerSpeed.Value += 1f;
        return false;
    }
}
