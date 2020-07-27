using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFalling : StateBase
{
    Transform fallPoint;

    public StateFalling(Player player, Transform fallPoint) : base(player)
    {
        this.fallPoint = fallPoint;
    }

    public override void FixedTick()
    {
           
    }

    public override void Tick()
    {
        player.transform.position = Vector2.MoveTowards(player.transform.position, fallPoint.position, 5f * Time.deltaTime);
    }

    public override void OnStateEnter()
    {
        player.Die();
        player.Anim.SetTrigger("Caida");
        player.StopMovement();
    }
}
