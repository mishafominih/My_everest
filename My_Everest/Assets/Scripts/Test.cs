using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private bool canCut;
    [SerializeField] private float timeToCut;
    [SerializeField] private GameObject logPrefab;
    private float _timeToCut;
    private Coroutine cuttingTheTree;

    private void Start()
    {
        _timeToCut = timeToCut;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canCut = true;
            Debug.Log(canCut.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canCut = false;
    }
    public void StartCutting()
    {
        cuttingTheTree = StartCoroutine("CuttingTheTree");
    }

    public void StopCutting()
    {
        if (cuttingTheTree != null)
        {
            _timeToCut = timeToCut;
            StopCoroutine("CuttingTheTree");
            Debug.Log("Рубка остановилась");
            cuttingTheTree = null;
        }
    }
    IEnumerator CuttingTheTree()
    {
        while (_timeToCut > 0)
        {
            Debug.Log(_timeToCut);
            _timeToCut -= Time.deltaTime;
            yield return null;
        }
        Instantiate(logPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Debug.Log("Срубил");
    }
    
    public void OnClickDown()
    {
        if (canCut)
        {
            StartCutting();
        }
    }

    public  void OnExit()
    {
        StopCutting();
    }
}
