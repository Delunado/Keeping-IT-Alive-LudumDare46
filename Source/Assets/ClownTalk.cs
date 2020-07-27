using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClownTalk : MonoBehaviour
{
    [SerializeField] IntSO day;
    [SerializeField] IntSO totalKids;
    [SerializeField] IntSO actualKids;

    [SerializeField] GameObject panelClown;
    [SerializeField] GameObject dialogClown;
    [SerializeField] GameObject tecla;
    [SerializeField] GameObject tecla2;
    [SerializeField] GameObject imagenFinalBueno;
    [SerializeField] GameObject imagenFinalMalo;
    [SerializeField] GameObject uiCoin;
    [SerializeField] TextMeshProUGUI textoMalo;
    [SerializeField] TextMeshProUGUI textoBueno;
    [SerializeField] TextMeshProUGUI textoNormal;


    private void Start()
    {
        if (day.Value == 1)
            tecla.SetActive(false);

        if (day.Value > 1)
        {
            dialogClown.SetActive(true);
            tecla.SetActive(false);
        }
        if (day.Value == 8)
        {
            dialogClown.SetActive(false);
            tecla2.SetActive(true);

            textoNormal.gameObject.SetActive(false);

            if (actualKids.Value >= totalKids.Value)
            {
                textoBueno.gameObject.SetActive(true);
            }
            else
            {
                textoMalo.gameObject.SetActive(true);
            }
        }
    }

    public void ActivePanel(bool active)
    {
        if (tecla)
            Destroy(tecla);

        if (!active && day.Value != 8)
        {
            dialogClown.SetActive(true);
        }

        if (!active && day.Value >= 8)
        {
            uiCoin.SetActive(false);
            if (actualKids.Value >= totalKids.Value)
            {
                if (!imagenFinalBueno.gameObject.activeSelf)
                    imagenFinalBueno.gameObject.SetActive(true);

            }
            else
            {
                if (!imagenFinalMalo.gameObject.activeSelf)
                    imagenFinalMalo.gameObject.SetActive(true);

            }
        }

        if (active && day.Value >= 8)
        {
            if (imagenFinalBueno.gameObject.activeSelf)
            {
                SceneManager.LoadScene("Start");
                return;
            }

            if (imagenFinalMalo.gameObject.activeSelf)
            {
                SceneManager.LoadScene("Start");
                return;
            }
        }

        panelClown.SetActive(active);
        FindObjectOfType<StartLevel>().canStartLevel = true;
    }
}
