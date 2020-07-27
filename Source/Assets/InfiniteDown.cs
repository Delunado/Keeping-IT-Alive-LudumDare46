using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDown : MonoBehaviour
{
    [SerializeField] private FloatSO velocity;

    private bool canMove;
    public bool CanMove { get => canMove; set => canMove = value; }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.position += Vector3.down * velocity.Value * Time.deltaTime;
    }
}
