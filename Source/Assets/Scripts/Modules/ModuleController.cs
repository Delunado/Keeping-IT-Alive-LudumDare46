using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : MonoBehaviour
{
    [SerializeField] private List<GameObject> moduleList;
    [SerializeField] private Vector3 positionModule;
    [SerializeField] private Vector3 spawnPositionModules;
    private Vector3 lastPositionModule;
    private GameObject lastInstantiatedModule;
    private bool firstTime = true;

    [SerializeField] private IntSO maxModules;
    private int actualModules;
    [SerializeField] private GameObject finalModule;

    private float distanceModule = 10.7855f;
    private ModulePool modulePool;
    private HashSet<int> activeModuleSet;

    public HashSet<int> ActiveModuleSet { get => activeModuleSet; set => activeModuleSet = value; }

    private void Awake()
    {
        modulePool = new ModulePool();
        activeModuleSet = new HashSet<int>();
    }

    void Start()
    {
        actualModules = maxModules.Value;
    }

    public void InitialSpawnModule()
    {
        for (int i = 0; i < 4; i++)
        {
            InstantiateInitialsModules();
        }
    }

    public void SpawnModule()
    {
        InstantiateModule();
    }

    public void InstantiateModule()
    {
        GameObject randomModule = RandomModule();
        GameObject moduleObject;

        lastPositionModule = lastInstantiatedModule.transform.position + new Vector3(0, distanceModule, 0);

        if (actualModules > 0)
        {
            moduleObject = Instantiate(randomModule, lastPositionModule, Quaternion.identity).gameObject;

            lastInstantiatedModule = moduleObject;

            Module module = moduleObject.GetComponent<Module>();
            module.ModuleController = this;

            ModuleMovement moduleMov = moduleObject.GetComponent<ModuleMovement>();
            moduleMov.CanMove = true;

            activeModuleSet.Add(module.Id);

            actualModules--;
        }
        else
        {
            moduleObject = Instantiate(finalModule, lastPositionModule, Quaternion.identity).gameObject;

            lastInstantiatedModule = moduleObject;

            Module module = moduleObject.GetComponent<Module>();
            module.ModuleController = this;

            activeModuleSet.Add(module.Id);

            ModuleMovement moduleMov = moduleObject.GetComponent<ModuleMovement>();
            moduleMov.CanMove = true;
        }
    }


    public void InstantiateInitialsModules()
    {
        if (firstTime)
        {
            firstTime = false;
            lastPositionModule = positionModule;
        }
        else
        {
            lastPositionModule = lastInstantiatedModule.transform.position + new Vector3(0, distanceModule, 0);
        }

        GameObject randomModule = RandomModule();

        GameObject moduleObject = Instantiate(randomModule, lastPositionModule, Quaternion.identity).gameObject;
        lastInstantiatedModule = moduleObject;

        Module module = moduleObject.GetComponent<Module>();
        module.ModuleController = this;

        ModuleMovement moduleMov = moduleObject.GetComponent<ModuleMovement>();
        moduleMov.CanMove = true;

        activeModuleSet.Add(module.Id);
        modulePool.AddModule(module.Id, moduleObject);

        actualModules--;
    }

    public GameObject RandomModule()
    {
        int random = Random.Range(0, moduleList.Count);
        GameObject moduleGO = moduleList[random];
        Module module = moduleGO.GetComponent<Module>();

        while (activeModuleSet.Contains(module.Id))
        {
            random = Random.Range(0, moduleList.Count);
            moduleGO = moduleList[random];
            module = moduleGO.GetComponent<Module>();
        }

        return moduleGO;
    }
}
