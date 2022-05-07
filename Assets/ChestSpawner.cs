using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ChestSpawnPoint
    {
        public Vector2 center, size;
    }

    public List<ChestSpawnPoint> spawnpoints;

    [SerializeField]
    GameObject chest;

    #region Singleton
    public static ChestSpawner instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public bool chestSpawned = false;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        currentWeapon = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        if(!chestSpawned)
        {
            int rand = Random.Range(0, spawnpoints.Count);
            Vector2 pos = Center(rand) + new Vector2(Random.Range(-Size(rand).x / 2, Size(rand).x / 2), Random.Range(-Size(rand).y / 2, Size(rand).y / 2));
            Instantiate(chest, pos, Quaternion.identity);
            chestSpawned = true;
        }
        //Debug.Log("chest spawned " + Center(rand) + " " + Size(rand));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Spawn());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        foreach(ChestSpawnPoint spawnpoint in spawnpoints)
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
