using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigDay : MonoBehaviour
{
    [SerializeField] private IntSO day;
    [SerializeField] private FloatSO moduleSpeed;
    [SerializeField] private IntSO combinationsNumber;

    [SerializeField] private IntSO actualKidsNumber;
    [SerializeField] private IntSO totalKidsNumber;

    [SerializeField] private GameObject upgradeMachine;

    private void Start()
    {
        if (day.Value == 0)
        {
            //Escena inicial
        } else
        {
            upgradeMachine.SetActive(true);
        }

        day.Value++;

        if (day.Value % 2 == 0)
        {
            moduleSpeed.Value += 0.15f;
        }

        if (day.Value % 2 == 0)
        {
            combinationsNumber.Value++;
        }

        if (actualKidsNumber.Value >= totalKidsNumber.Value)
        {
            if (day.Value < 7)
            {
                //Juego pasado rapido
            } else
            {
                //Juego pasado normal
            }
        }

        if (day.Value == 7 && actualKidsNumber.Value < totalKidsNumber.Value)
        {
            //Juego fallido
        }
    }
}
