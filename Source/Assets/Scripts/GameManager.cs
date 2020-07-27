using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameStarted = false;
    public bool GameStarted { get => gameStarted; set => gameStarted = value; }

    [SerializeField] private GameObject deadScreen;

    private bool died;

    public void StartGame()
    {
        gameStarted = true;

        ModuleMovement[] activeModules = FindObjectsOfType<ModuleMovement>();

        foreach (ModuleMovement m in activeModules)
            m.CanMove = true;

        FindObjectOfType<ModuleController>().InitialSpawnModule();
    }

    public void FinishGame()
    {
        ModuleMovement[] activeModules = FindObjectsOfType<ModuleMovement>();

        foreach (ModuleMovement m in activeModules)
            m.CanMove = false;
    }

    public void Dead()
    {
        StartCoroutine(DeadCorroutine());
    }

    private IEnumerator DeadCorroutine()
    {
        yield return new WaitForSeconds(1f);

        if (deadScreen)
            deadScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        died = true;
    }

    private void Update()
    {
        if (died)
        {
            if (Input.GetButtonDown("Interact"))
            {
                SceneManager.LoadScene("Main");
                //Sounds
            }
        }
    }

}
