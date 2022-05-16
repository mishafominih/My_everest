using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Log, Stone, Gold, Meet, Skin
}

public static class ResourceManager
{
    public static Dictionary<Resource, int> GetInventory()
    {
        var inventory = new Dictionary<Resource, int>();
        var resources = new List<Resource> { Resource.Log, Resource.Stone, Resource.Gold, Resource.Meet, Resource.Skin };
        foreach(var res in resources)
        {
            var count = GetResource(res);
            inventory[res] = count;
        }
        return inventory;
    }

    public static int GetResource(Resource res)
    {
        return PlayerPrefs.GetInt(res.ToString(), 0);
    }

    public static void AddResources(Dictionary<Resource, int> inventory)
    {
        foreach(var res in inventory.Keys)
        {
            var count = GetResource(res);
            count += inventory[res];
            SaveResource(res, count);
        }
    }

    public static void SaveResource(Resource res, int count)
    {
        PlayerPrefs.SetInt(res.ToString(), count);
    }
}
