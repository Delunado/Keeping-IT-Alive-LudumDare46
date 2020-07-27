using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombinationsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI combinationsText;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>(); 
    }

    public void Active(bool active)
    {
        if (active)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.8f);
        } else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
    }

    public void ResetText()
    {
        combinationsText.text = "";
    }

    public void SetText(string text)
    {
        combinationsText.text = text;
    }
}
