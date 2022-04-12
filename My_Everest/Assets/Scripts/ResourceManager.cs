using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Wood, Stone, Gold
}

public class ResourceManager
{
    public Dictionary<Resource, int> GetInventory()
    {
        var inventory = new Dictionary<Resource, int>();
        var resources = new List<Resource> { Resource.Wood, Resource.Stone, Resource.Gold };
        foreach(var res in resources)
        {
            var count = GetResource(res);
            inventory[res] = count;
        }
        return inventory;
    }

    public int GetResource(Resource res)
    {
        return PlayerPrefs.GetInt(res.ToString(), 0);
    }

    public void AddResources(Dictionary<Resource, int> inventory)
    {
        foreach(var res in inventory.Keys)
        {
            var count = GetResource(res);
            count += inventory[res];
            SaveResource(res, count);
        }
    }

    public void SaveResource(Resource res, int count)
    {
        PlayerPrefs.SetInt(res.ToString(), count);
    }
}
