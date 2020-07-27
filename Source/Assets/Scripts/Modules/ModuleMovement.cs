using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMovement : MonoBehaviour
{
    [SerializeField] private FloatSO velocity;

    private bool canMove = false;
    public bool CanMove { get => canMove; set => canMove = value; }

    void Update()
    {
        if (canMove)
            transform.position += Vector3.down * velocity.Value * Time.deltaTime;
    }
}
