using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownTalk : MonoBehaviour
{
    [SerializeField] IntSO day;

    [SerializeField] GameObject panelClown;
    [SerializeField] GameObject dialogClown;

    private void Start()
    {
        if (day.Value > 0)
        {
            dialogClown.SetActive(true);
        }
    }

    public void ActivePanel(bool active)
    {
        if (active)
            dialogClown.SetActive(true);

        panelClown.SetActive(active);
        FindObjectOfType<StartLevel>().canStartLevel = true;
    }
}
