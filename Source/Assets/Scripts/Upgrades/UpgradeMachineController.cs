using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMachineController : MonoBehaviour
{
    [SerializeField] private IntSO nextUpgradePrice;
    [SerializeField] private IntSO actualPayedCoins;

    private List<UpgradeBase> upgradesList;
    [SerializeField] private FloatSO playerSpeed;
    [SerializeField] private IntSO maxModules;
    [SerializeField] private IntSO maxErrors;

    [SerializeField] private TextMeshPro coinsText;
    [SerializeField] private TextMeshPro upgradeText;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject coinPanel;

    private AudioSource audioSource;
    public AudioClip upgradeClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        coinsText.text = (nextUpgradePrice.Value - actualPayedCoins.Value).ToString();
        upgradesList = new List<UpgradeBase>();

        //We create all the updates
        UpgradeSpeed upgradeSpeed = new UpgradeSpeed("Now you walk faster... Don't trip!", 1, 4, playerSpeed);
        TimeUpgrade upgradeTime = new TimeUpgrade("Your days will last longer.", 1, 4, maxModules);
        ErrorUpgrade upgradeError = new ErrorUpgrade("One more chance on minigames.", 1, 4, maxErrors);
        upgradesList.Add(upgradeSpeed);
        upgradesList.Add(upgradeTime);
        upgradesList.Add(upgradeError);
    }

    public void AddCoin()
    {
        if (upgradesList.Count == 0)
        {
            upgradePanel.SetActive(false);
            coinPanel.SetActive(false);
            return;
        }

        if (upgradePanel.activeSelf)
        {
            coinsText.text = (nextUpgradePrice.Value - actualPayedCoins.Value).ToString();
            coinPanel.SetActive(true);
            upgradePanel.SetActive(false);
        } else
        {
            actualPayedCoins.Value++;
            coinsText.text = (nextUpgradePrice.Value - actualPayedCoins.Value).ToString();
            CheckBuyedUpgrade();
        }

        FindObjectOfType<UICoins>().UpdateUI();
    }

    private void CheckBuyedUpgrade()
    {
        if (actualPayedCoins.Value == nextUpgradePrice.Value)
        {
            audioSource.PlayOneShot(upgradeClip);

            actualPayedCoins.Value = 0;
            UpgradeBase upgrade = upgradesList[Random.Range(0, upgradesList.Count)];

            if (upgrade.Upgrade())
            {
                upgradesList.Remove(upgrade);
            }

            upgradePanel.SetActive(true);
            coinPanel.SetActive(false);
            upgradeText.text = upgrade.UpgradeName;

            //Calculate the new price
            float newPrice = nextUpgradePrice.Value * 1.4f;
            nextUpgradePrice.Value = Mathf.RoundToInt(newPrice);
        }
    }
}
