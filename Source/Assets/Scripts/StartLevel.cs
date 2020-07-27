using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private IntSO day;
    public bool canStartLevel;

    private void Start()
    {
        if (day.Value > 1)
            canStartLevel = true;

        if (day.Value > 7)
            canStartLevel = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canStartLevel)
                SceneManager.LoadScene("Level");
        }
    }
}
