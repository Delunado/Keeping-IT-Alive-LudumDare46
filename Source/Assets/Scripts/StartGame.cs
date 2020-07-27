using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private IntSO day;
    [SerializeField] private IntSO actualKids;
    [SerializeField] private IntSO coins;
    [SerializeField] private IntSO combinationsNumber;
    [SerializeField] private IntSO maxModules;
    [SerializeField] private IntSO totalKids;
    [SerializeField] private IntSO actualPayedCoins;
    [SerializeField] private IntSO nextUpgradePrice;
    [SerializeField] private IntSO maxErrors;
    [SerializeField] private FloatSO moduleSpeed;
    [SerializeField] private FloatSO playerSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            day.Value = 0;
            actualKids.Value = 0;
            coins.Value = 0;
            combinationsNumber.Value = 3;
            maxModules.Value = 7;
            moduleSpeed.Value = 0.85f;
            playerSpeed.Value = 5f;
            totalKids.Value = 40;
            actualPayedCoins.Value = 0;
            nextUpgradePrice.Value = 5;
            maxErrors.Value = 0;

            SceneManager.LoadScene("Main");
        }
    }
}
