using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }

    public int maxSpawn = 0;
    public int poolCount = 30;

    #region Singleton
    public static EnemySpawner instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public List<Pool> pools;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // update is called once per frame
    private void FixedUpdate()
    {
        
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        int rand = Random.Range(0, pools.Count);
        int count = Random.Range(1, 3);
        SpawnFromList(rand, count);
        StartCoroutine(Spawn());
    }

    void SpawnFromList(int index, int count)
    {
        for (int i=0;i< count; ++i)
        {
            Instantiate(pools[index].prefab, FindRandomSpawnPoint().position, Quaternion.identity);
            maxSpawn++;
        }
    }

    Transform FindRandomSpawnPoint()
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int rand = Random.Range(0, spawnpoints.Length);

        return spawnpoints[rand].transform;
    }
}
