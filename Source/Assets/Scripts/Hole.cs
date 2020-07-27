using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform holePoint;

    public Transform GetHolePoint()
    {
        return holePoint;
    }
}
