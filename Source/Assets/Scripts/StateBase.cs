using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StateBase
{
    protected Player player;

    public abstract void Tick();
    public abstract void FixedTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public StateBase(Player player)
    {
        this.player = player;
    }
}
