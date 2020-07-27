using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMovement : StateBase
{
    public StateMovement(Player player) : base(player)
    {
    }

    public override void FixedTick()
    {
        if (player.Input.vertical > 0)
        {
            player.Anim.SetBool("walkVerticalUp", true);
            player.Anim.SetBool("walkVerticalDown", false);
            player.Anim.SetBool("LadoYArriba", true);
        } else if (player.Input.vertical < 0)
        {
            player.Anim.SetBool("walkVerticalUp", false);
            player.Anim.SetBool("walkVerticalDown", true);
            player.Anim.SetBool("LadoYAbajo", true);
        } else
        {
            player.Anim.SetBool("walkVerticalUp", false);
            player.Anim.SetBool("walkVerticalDown", false);

            if (player.Input.horizontal > 0)
            {
                player.Anim.SetBool("walkLado", true);
                player.SpriteRenderer.flipX = false;
                player.Anim.SetBool("LadoYAbajo", false);
            }
            else if (player.Input.horizontal < 0)
            {
                player.Anim.SetBool("walkLado", true);
                player.Anim.SetBool("LadoYAbajo", false);
                player.SpriteRenderer.flipX = true;
            }
            else
            {
                player.Anim.SetBool("walkLado", false);
            }
        }

        if (!(player.Input.horizontal != 0 && player.Input.vertical != 0))
        {
            player.lastDirection = new Vector2(player.Input.horizontal, player.Input.vertical);
        }

        player.Move();

        if (player.CheckKid())
        {
            player.ChangeState(new StateCombination(player, StateCombination.CombinationMode.KID));
            return;
        }

        if (player.CheckDestructibleObject())
        {
            player.ChangeState(new StateCombination(player, StateCombination.CombinationMode.DESTRUCTION));
            return;
        }


        player.CheckBasket();

        if (!player.CheckMovement())
        {
            player.ChangeState(new StateIdle(player));
            return;
        }
    }

    public override void Tick()
    {
        
    }

    public override void OnStateEnter()
    {
        player.transform.SetParent(null);
    }

    public override void OnStateExit()
    {
        player.Anim.SetBool("walkVerticalUp", false);
        player.Anim.SetBool("walkVerticalDown", false);
        player.Anim.SetBool("LadoYArriba", false);
        player.Anim.SetBool("LadoYAbajo", false);
        player.Anim.SetBool("walkLado", false);
    }
}
