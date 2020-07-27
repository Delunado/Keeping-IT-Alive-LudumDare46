using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField] LayerMask objectLayerMask;

    public bool CanBePlaced()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y / 2f, objectLayerMask);

        return !raycast;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            FindObjectOfType<Player>().Die();
        }
    }
}
