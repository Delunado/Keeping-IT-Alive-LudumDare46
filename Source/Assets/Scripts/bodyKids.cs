using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyKids : MonoBehaviour
{
    [SerializeField] private IntSO day;
    [SerializeField] private List<GameObject> bodysList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {


        //int dayBody = day.Value;

        if (day.Value > 1)
        {
            for (int i = 0; i < day.Value - 1; i++)
            {
                bodysList[i].gameObject.SetActive(true);
            }
        }

    }
}
