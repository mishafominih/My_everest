using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ObjectsInteractor : MonoBehaviour
{
    private  List<Beepka> beepke;
    private static ObjectsInteractor instance;

    private void Start()
    {
        instance = this;
        beepke = FindObjectsOfType<Beepka>().ToList();
        var distance = Vector3.Distance(this.transform.position, beepke[0].transform.position);
        Debug.Log("Всего бипок" + beepke.Count + "\n Дистанция до первого обьекта" + distance);
    }

    public void AddBeepka(Beepka beepka)
    {
        beepke.Add(beepka);
    }
    public void RemoveBeepka(Beepka beepka)
    {
        if (beepke != null)
        {
            beepke.Remove(beepka);
        }
    }
    public static ObjectsInteractor GetInstance()
    {
        if (instance == null)
            instance = new ObjectsInteractor();
        return instance;
    }
    private void Update()
    {
        foreach (var beepka in beepke)
        {
            var distance = Vector2.Distance(this.transform.localPosition, beepka.transform.localPosition);
            if (Math.Round(beepka.TriggerRadius,1) >= Math.Round(distance,1) )
            {
                beepka.CanMine = true;
            }
            else
            {
                beepka.CanMine = false;
            }
        }
    }
}
