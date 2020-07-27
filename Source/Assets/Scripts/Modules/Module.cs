using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int score;
    [SerializeField] private List<GameObject> coinList;
    [SerializeField] private List<GameObject> kidList;
    private ModuleController moduleController;

    private float coinProb;
    private float kidProb;

    public int Id { get => id; set => id = value; }
    public ModuleController ModuleController { get => moduleController; set => moduleController = value; }

    private void Start()
    {
        moduleController = FindObjectOfType<ModuleController>();

        coinProb = 0.85f;
        kidProb = 0.95f;

        if (coinList.Count > 0)
        {
            int count = coinList.Count;

            for (int i = 0; i < count; i++)
            {
                if (Random.value <= coinProb)
                {
                    //Instanciar moneda
                    GameObject coin = coinList[Random.Range(0, coinList.Count)];
                    coin.SetActive(true);
                    coinList.Remove(coin);
                    coinProb -= 0.10f;
                }
            }
        }

        if (kidList.Count > 0)
        {
            int count = kidList.Count;

            for (int i = 0; i < count; i++)
            {
                if (Random.value <= kidProb)
                {
                    //Instanciar kid               
                    GameObject kid = kidList[Random.Range(0, kidList.Count)];
                    kid.SetActive(true);
                    kidList.Remove(kid);
                    kidProb -= 0.5f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            moduleController.ActiveModuleSet.Remove(id);
            moduleController.SpawnModule();
            Destroy(gameObject);
        }
    }

}
