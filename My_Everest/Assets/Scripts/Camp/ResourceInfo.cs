using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfo : MonoBehaviour
{
    private Text info;

    void Start()
    {
        info = GetComponent<Text>();
        SetResource();
    }

    private void Update()
    {
        SetResource();
    }

    public void SetResource()
    {
        var result = "Ресурсы: \n";
        var all = ResourceManager.GetInventory();
        foreach(var res in all.Keys)
        {
            result += $"\n{res.ToString()}: {all[res]}";
        }
        info.text = result;
    }
}
