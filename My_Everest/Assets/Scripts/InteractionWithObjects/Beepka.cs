using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Beepka : MonoBehaviour
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private GameObject objectAfterMinePrefab;
    [SerializeField] private GameObject anim;
    [SerializeField] private int resourcesAmount;
    [SerializeField] private float timeToMine;
    [SerializeField] private float triggerRadius;
    public float TriggerRadius => triggerRadius;
    public bool CanMine { get; set; }
    private float _timeToMine;
    private Coroutine miningRoutine;

    #region EventTriggerMethods
    
    public void OnClickDown()
    {
        if (CanMine)
        {
            StartMining();
        }
    }

    public  void OnExit()
    {
        StopMining();
    }
    
    #endregion
    
    private void Start()
    {
        CanMine = false;
        _timeToMine = timeToMine;
    }
    
    private void StartMining()
    {
        anim.SetActive(true);
        miningRoutine = StartCoroutine("MiningRoutine");
    }

    private void StopMining()
    {
        if (miningRoutine != null)
        {
            _timeToMine = timeToMine;
            StopCoroutine("MiningRoutine");
            Debug.Log("Рубка остановилась");
            anim.SetActive(false);
            miningRoutine = null;
        }
    }
    private IEnumerator MiningRoutine()
    {
        while (_timeToMine > 0)
        {
            _timeToMine -= Time.deltaTime;
            yield return null;
        }
        SpawnResources();
        Destroy(this.gameObject);
        miningRoutine = null;

        if (objectAfterMinePrefab != null)
        {
            Instantiate(objectAfterMinePrefab, transform.position, Quaternion.identity);
            // Добавить новый обьект в список бипок в ObjectsInteractor
        }


        ObjectsInteractor.GetInstance().RemoveBeepka(this);
        
        Debug.Log("Срубил");
    }

    private void SpawnResources()
    {
        for (int i = 0; i < resourcesAmount; i++)
            Instantiate(resourcePrefab, transform.position - Random.insideUnitSphere , Quaternion.identity);
    }
}
