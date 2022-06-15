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
        var lavel = PlayerPrefs.GetInt("PlayerLavel", 1);
        SceneManager.LoadScene(lavel);
    }

    public void Home()
    {
        var inventory = GameObject.FindObjectsOfType<UIInventory>();
        if ( !( inventory is null))
            foreach(var inv in inventory)
            {
                ResourceManager.AddResources(inv.GetAllItems());
            }
        SceneManager.LoadScene(0);
    }

    public void Finish()
    {
        var inventory = GameObject.FindObjectsOfType<UIInventory>();
        foreach (var inv in inventory)
        {
            ResourceManager.AddResources(inv.GetAllItems());
        }
        var lavel = PlayerPrefs.GetInt("PlayerLavel", 1);
        PlayerPrefs.SetInt("PlayerLavel", lavel + 1);
        SceneManager.LoadScene(0);
    }

    public void Revert()
    {
        PlayerPrefs.DeleteAll();
    }
}
