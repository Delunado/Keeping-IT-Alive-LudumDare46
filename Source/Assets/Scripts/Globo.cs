using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Globo : MonoBehaviour
{
    Image spriteRenderer;
    List<Color> colorList = new List<Color>();

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Color32 color1 = new Color(1, 0, 0, 1);
        Color32 color2 = new Color(1, 0, 1, 1);
        Color32 color3 = new Color(0, 1, 1, 1);
        Color32 color4 = new Color(0, 0, 1, 1);
        Color32 color5 = new Color(1, 0.92f, 0.016f, 1);
        colorList.Add(color1);
        colorList.Add(color2);
        colorList.Add(color3);
        colorList.Add(color4);
        colorList.Add(color5);
        spriteRenderer.color = colorList[Random.Range(0,colorList.Count)];
    }

}
