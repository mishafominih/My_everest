using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camp : MonoBehaviour
{
    public GameObject Main;
    public GameObject Fire;
    public void Exit()
    {
        Application.Quit();
    }

    public void Trip()
    {
        SceneManager.LoadScene(1);
    }
    public void Training()
    {
        SceneManager.LoadScene("Misha");
    }

    public void Home()
    {
        var inventory = GameObject.FindObjectsOfType<UIInventory>();
        foreach(var inv in inventory)
        {
            ResourceManager.AddResources(inv.GetAllItems());
        }
        SceneManager.LoadScene(0);
    }

    public void Craft()
    {
        
    }
}
