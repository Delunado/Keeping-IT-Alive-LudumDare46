using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFinish : StateBase
{
    public StateFinish(Player player) : base(player)
    {
    }

    public override void FixedTick()
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        player.Anim.SetBool("Idle", true);
        player.StopMovement();
    }
}
