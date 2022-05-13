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
        public int count;
    }
    [System.Serializable]
    public class BossPool
    {
        public string tag;
        public GameObject prefab;
    }


    [System.Serializable]
    public class EnemySpawnPoint
    {
        public Vector2 center, size;
    }


    public int maxSpawn = 0;

    [SerializeField]
    private int spawnLimit = 30, previousSpawn = -1;
    [SerializeField]
    float spawnFrequency = 2.5f;


    #region Singleton
    public static EnemySpawner instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<EnemySpawnPoint> spawnpoints;
    public List<Pool> pools;
    public List<BossPool> m_bossPool;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    public void SpawnBoss()
    {
        int index = Random.Range(0, m_bossPool.Count);
        int spawn = Random.Range(0, spawnpoints.Count);
        Vector2 pos = Center(spawn) 
            + new Vector2(Random.Range(-Size(spawn).x / 2, Size(spawn).x / 2), 
            Random.Range(-Size(spawn).y / 2, Size(spawn).y / 2));
        float rp = Random.Range(0.9f, 1.1f);
        Instantiate(m_bossPool[index].prefab, pos * rp, Quaternion.identity);
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnFrequency);
        int rand = Random.Range(0, pools.Count);
        while (rand == previousSpawn) rand = Random.Range(0, pools.Count);
        StartCoroutine(SpawnFromList(rand));
        previousSpawn = rand;
        StartCoroutine(Spawn());
    }

    IEnumerator SpawnFromList(int index)
    {
        int rand = Random.Range(0, spawnpoints.Count);
        Vector2 pos = Center(rand) + new Vector2(Random.Range(-Size(rand).x / 2, Size(rand).x / 2), Random.Range(-Size(rand).y / 2, Size(rand).y / 2));
        for (int i=0;i< pools[index].count; ++i)
        {
            if (maxSpawn < spawnLimit)
            {
                float rp = Random.Range(0.9f, 1.1f);
                Instantiate(pools[index].prefab, pos * rp, Quaternion.identity);
                maxSpawn++;
                yield return null;
            }   
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(2, 2, 2, 0.5f);
        foreach (EnemySpawnPoint spawnpoint in spawnpoints)
        {
            Gizmos.DrawCube(spawnpoint.center, spawnpoint.size);
        }
    }
    Vector2 Center(int idx)
    {
        return spawnpoints[idx].center;
    }
    Vector2 Size(int idx)
    {
        return spawnpoints[idx].size;
    }
}
