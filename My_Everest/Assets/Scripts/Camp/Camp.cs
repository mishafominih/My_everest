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
        Debug.LogError("1");
        Application.Quit();
    }

    public void Trip()
    {
        Debug.LogError("2");
        SceneManager.LoadScene(1);
    }

    public void Craft()
    {
        Main.SetActive(false);
        Fire.SetActive(true);
    }
}
