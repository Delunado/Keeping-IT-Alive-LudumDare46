using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDayOne : MonoBehaviour
{
    public IntSO day;

    void Start()
    {
        if (day.Value > 1)
            Destroy(gameObject);
    }
}
