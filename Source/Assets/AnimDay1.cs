using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDay1 : MonoBehaviour
{
    [SerializeField] IntSO day;

    // Start is called before the first frame update
    void Start()
    {
        if (day.Value != 0)
        {
            Destroy(gameObject);
        }
    }
}
