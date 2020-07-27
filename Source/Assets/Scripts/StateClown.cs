using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateClown : StateBase
{
    public StateClown(Player player) : base(player)
    {
    }

    public override void FixedTick()
    {
        if (player.Input.horizontal != 0 || player.Input.vertical != 0 || player.Input.interact)
        {
            ClownTalk clown = player.GetClownTalk();
            clown.ActivePanel(false);

            player.ChangeState(new StateIdle(player));
        }
    }

    public override void Tick()
    {
        
    }
}
