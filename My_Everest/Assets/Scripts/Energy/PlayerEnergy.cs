using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEnergy : Energy
{
    private float DeltaEnergy = 9;

    private void Start()
    {
        Capacity = Capacity * UpgradeInfo.Energy;
        value = Capacity;
        text = transform
            .GetComponentsInChildren<Text>()
            .Where(x => x.name == "Energy")
            .FirstOrDefault();
        if (text != null)
        {
            text.text = $"Energy: {Mathf.Round(value)}%";
        }
    }

    public override void Death()
    {
        PlayerPrefs.DeleteAll();
        base.Death();
        SceneManager.LoadScene(0);
    }
    public override void ChangeEnergy(float delta = 1)
    {
        base.value -= delta * DeltaEnergy*Time.deltaTime;
        value = Mathf.Min(value, Capacity);
        value = Mathf.Max(value, 0);
        if (value == 0)
            Death();
        if (text != null)
        {
            text.text = $"Energy: {Mathf.Round(value)}%";
        }
    }
}
