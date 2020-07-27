using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tecla;
    [SerializeField] private GameObject dialog;

    private bool open;

    public void Open()
    {
        if (open)
        {
            tecla.SetActive(false);
            dialog.SetActive(true);
            open = false;
        } else
        {
            tecla.SetActive(true);
            dialog.SetActive(false);
            open = true;
        }
    }

}
