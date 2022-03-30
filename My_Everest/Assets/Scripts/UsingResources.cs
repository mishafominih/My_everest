using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class UsingResources : MonoBehaviour
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private GameObject objectAfterMinePrefab;
    [SerializeField] private GameObject anim;
    [SerializeField] private int resourcesAmount;
    [SerializeField] private float timeToMine;
    [SerializeField] private float triggerRadius;

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
        _timeToMine = timeToMine;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanMine = true;
            Debug.Log(CanMine.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CanMine = false;
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

        if (objectAfterMinePrefab != null)
            Instantiate(objectAfterMinePrefab, transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
        miningRoutine = null;
        Debug.Log("Срубил");
    }

    private void SpawnResources()
    {
        for (int i = 0; i < resourcesAmount; i++)
            Instantiate(resourcePrefab, transform.position - Random.insideUnitSphere , Quaternion.identity);
    }
}
