using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject On;
    public GameObject Off;

    public void Do()
    {
        On.SetActive(true);
        Off.SetActive(false);
    }
}
