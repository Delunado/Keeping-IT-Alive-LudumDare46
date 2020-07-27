using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IntSO")]
public class IntSO : ScriptableObject
{
    [SerializeField] private int value;
    public int Value { get => value; set => this.value = value; }
}
