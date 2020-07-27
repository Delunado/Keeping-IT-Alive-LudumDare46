using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase
{
    public StateIdle(Player player) : base(player)
    {
    }

    public override void FixedTick()
    {
        player.CheckBasket();

        if (player.CheckMovement())
        {
            player.ChangeState(new StateMovement(player));
            return;
        }

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

        if (player.CheckClown())
        {
            ClownTalk clown = player.GetClownTalk();
            clown.ActivePanel(true);
            player.ChangeState(new StateClown(player));
            return;
        }

        if (player.CheckOpenTutorial())
        {
            player.GetTutorial().Open();
        }

        if (player.CheckUpgradeMachine())
        {
            UpgradeMachineController controller = player.GetUpgradeMachine();

            if (controller)
            {
                if (player.Coins.Value > 0)
                {
                    player.audioSource.PlayOneShot(player.insertCoin);
                    player.Coins.Value--;
                    controller.AddCoin();
                }
            }
        }
    }

    public override void Tick()
    {
        
    }

    public override void OnStateEnter()
    {
        player.lastDirection = Vector2.up;
        player.Anim.SetBool("Idle", true);
        player.StopMovement();
    }

    public override void OnStateExit()
    {
        player.Anim.SetBool("Idle", false);
    }
}
