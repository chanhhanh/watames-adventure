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


    [System.Serializable]
    public class EnemySpawnPoint
    {
        public Vector2 center, size;
    }

    public List<EnemySpawnPoint> spawnpoints;

    public int maxSpawn = 0;

    [SerializeField]
    private int spawnLimit = 30, previousSpawn = -1;


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
        while (rand == previousSpawn) rand = Random.Range(1, 3);
        SpawnFromList(rand, count);
        previousSpawn = rand;
        StartCoroutine(Spawn());
    }

    void SpawnFromList(int index, int count)
    {
        int rand = Random.Range(0, spawnpoints.Count);
        Vector2 pos = Center(rand) + new Vector2(Random.Range(-Size(rand).x / 2, Size(rand).x / 2), Random.Range(-Size(rand).y / 2, Size(rand).y / 2));
        for (int i=0;i< count; ++i)
        {
            if (maxSpawn < spawnLimit)
            Instantiate(pools[index].prefab, pos, Quaternion.identity);
            maxSpawn++;
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
