using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCombination : StateBase
{
    int actualErrors;

    private int successfulButtonsNumber;
    private List<int> indexXbox = new List<int>();

    private string buttonsXbox = "ABXY";
    private string buttonsKeyboard = "KJLI";

    private string textCombination;


    public enum CombinationMode
    {
        KID,
        DESTRUCTION
    }

    private CombinationMode combinationMode;

    private enum buttonsKeys
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3,
        NONE = -1
    }

    private buttonsKeys keySelected;

    public StateCombination(Player player, CombinationMode combinationMode) : base(player)
    {
        this.combinationMode = combinationMode;
    }

    public override void OnStateEnter()
    {
        actualErrors = player.MaxErrors.Value;

        player.Anim.SetBool("Idle", true);
        player.CombinationsUI.Active(true);
        textCombination = "";
        player.StopMovement();
        successfulButtonsNumber = 0;
        GenerateCombination();
    }

    public override void OnStateExit()
    {
        player.CombinationsUI.ResetText();
        player.CombinationsUI.Active(false);
    }

    public override void Tick()
    {

    }

    public override void FixedTick()
    {
        InputButtons();

        if (successfulButtonsNumber == player.CombinationsNumber.Value)
        {
            player.ChangeState(new StateIdle(player));
            
            if (combinationMode == CombinationMode.DESTRUCTION)
            {
                player.DestructObject();
            } else if (combinationMode == CombinationMode.KID)
            {
                player.TakeKid();
            }          
            
            return;
        }
    }

    private void GenerateCombination()
    {
        for (int i = 0; i < player.CombinationsNumber.Value; i++)
        {
            int auxChar = Random.Range(0, 4);
            indexXbox.Add(auxChar);

            //Aqui hacer que si usas mando salga xbox y si no pc
            textCombination += buttonsKeyboard[auxChar];
        }

        //Aqui enlazar para que aparezca la combinacion en pantalla
        player.CombinationsUI.SetText(textCombination);
    }

    private void InputButtons()
    {
        if (successfulButtonsNumber != player.CombinationsNumber.Value)
        {
            if (player.Input.XLDown)
            {
                keySelected = buttonsKeys.X;
            }
            else if (player.Input.interact)
            {
                keySelected = buttonsKeys.A;
            }
            else if (player.Input.BJDown)
            {
                keySelected = buttonsKeys.B;
            }
            else if (player.Input.YIDown)
            {
                keySelected = buttonsKeys.Y;
            } else
            {
                keySelected = buttonsKeys.NONE;
            }

            if (keySelected != buttonsKeys.NONE)
            {
                
                if ((int)keySelected == indexXbox[successfulButtonsNumber])
                {
                    successfulButtonsNumber++;
                    string newCombinationText = "";

                    for (int i = 0; i < player.CombinationsNumber.Value; i++)
                    {
                        if (i < successfulButtonsNumber)
                            newCombinationText += "";
                        else
                            newCombinationText += buttonsKeyboard[indexXbox[i]];
                    }

                    player.CombinationsUI.SetText(newCombinationText);
                    player.audioSource.PlayOneShot(player.inputMinigame);
                }
                else if (actualErrors > 0)
                {
                    actualErrors--;
                    player.audioSource.PlayOneShot(player.failMinigame);
                } else
                {
                    player.audioSource.PlayOneShot(player.failMinigame);
                    player.ChangeState(new StateIdle(player));
                    return;
                }
            }
        }
    }


}
