using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePool
{
    private Dictionary<int, GameObject> modulePoolDictionary;

    public ModulePool()
    {
        modulePoolDictionary = new Dictionary<int, GameObject>();
    }

    public void AddModule(int key, GameObject module)
    {
        if (!modulePoolDictionary.ContainsKey(key))
            modulePoolDictionary.Add(key, module);
    }

    public bool ContainsModule(int key)
    {
        if(modulePoolDictionary.ContainsKey(key)) 
            return true;

        return false;
    }

    public GameObject GetModule(int key)
    {
        modulePoolDictionary.TryGetValue(key, out GameObject module);
        return module;
    }
}
