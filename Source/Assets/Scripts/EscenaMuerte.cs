using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscenaMuerte : MonoBehaviour
{
    [SerializeField] private IntSO day;
    [SerializeField] private TextMeshProUGUI dayText;


    // Start is called before the first frame update
    void Start()
    {
        dayText.text = day.Value.ToString();
    }

}
