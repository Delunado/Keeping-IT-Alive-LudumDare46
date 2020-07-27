using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    [SerializeField] private IntSO actualCoins;
    [SerializeField] private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = actualCoins.Value.ToString();
    }

    public void UpdateUI()
    {
        text.text = actualCoins.Value.ToString();
    }
}
