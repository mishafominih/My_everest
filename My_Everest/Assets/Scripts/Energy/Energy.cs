using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public float Capacity = 100;
    public float DeltaEnergy = 0.001f;

    protected float value;
    protected Text text;

    private void Start()
    {
        Capacity = PlayerPrefs.GetFloat("Energy", Capacity);
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

    public virtual void ChangeEnergy(float delta = 1)
    {
        value -= delta * DeltaEnergy;
        value = Mathf.Min(value, Capacity);
        value = Mathf.Max(value, 0);
        if (value == 0)
            Death();
        if (text != null)
        {
            text.text = $"Energy: {Mathf.Round(value)}%";
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
