using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FloatSO")]
public class FloatSO : ScriptableObject
{
    [SerializeField] private float value;

    public float Value { get => value; set => this.value = value; }
}
