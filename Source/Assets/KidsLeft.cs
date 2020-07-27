using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KidsLeft : MonoBehaviour
{
    public IntSO totalKids;
    public IntSO actualKids;

    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        if ((totalKids.Value - actualKids.Value) < 0)
            text.text = "" + 0;
        else
            text.text = (totalKids.Value - actualKids.Value).ToString();
    }

}
