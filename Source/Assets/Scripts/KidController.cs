using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shirtRenderer;
    [SerializeField] private SpriteRenderer pantsRenderer;
    [SerializeField] private SpriteRenderer hairRenderer;
    [SerializeField] private SpriteRenderer shoesRenderer;

    [SerializeField] private Color[] shirtColors;
    [SerializeField] private Color[] pantsColors;
    [SerializeField] private Color[] hairColors;
    [SerializeField] private Color[] shoesColors;

    // Start is called before the first frame update
    void Start()
    {
        shirtRenderer.color = shirtColors[Random.Range(0, shirtColors.Length)];
        pantsRenderer.color = pantsColors[Random.Range(0, pantsColors.Length)];
        hairRenderer.color = hairColors[Random.Range(0, hairColors.Length)];
        shoesRenderer.color = shoesColors[Random.Range(0, shoesColors.Length)];
    }

}
