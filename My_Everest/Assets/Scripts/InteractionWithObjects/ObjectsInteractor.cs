using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsInteractor : MonoBehaviour
{
    private List<Beepka> beepke;

    private void Start()
    {
        beepke = FindObjectsOfType<Beepka>().ToList();
        var distance = Vector3.Distance(this.transform.position, beepke[0].transform.position);
        Debug.Log("Всего бипок" + beepke.Count + "\n Дистанция до первого обьекта" + distance);
        
    }

    private void Update()
    {
        beepke = FindObjectsOfType<Beepka>().ToList();
        foreach (var beepka in beepke)
        {
            var distance = Vector3.Distance(this.transform.localPosition, beepka.transform.localPosition);
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
