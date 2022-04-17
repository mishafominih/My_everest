using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergy : Energy
{
    private float DeltaEnergy = 10;
    public override void Death()
    {
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
