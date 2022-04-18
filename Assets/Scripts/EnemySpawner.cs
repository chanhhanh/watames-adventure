using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    float randX;
    float randY;

    //random range
    public float Xstart = -12f;
    public float Xend = 12f;
    public float Ystart = -2f;
    public float Yend = 2f;

    Vector3 whereToSpawn;
    public float spawnCooldown = 2f;
    public float timeToNextSpawn = 0.0f;
    public float Count = 3;

    // Update is called once per frame
    void Update()
    {
        //updateSpawnPos();
        spawn();
    }
    private void spawn()
    {
        randX = Random.Range(Xstart, Xend);
        randY = Random.Range(Ystart, Yend);
        whereToSpawn = new Vector3(transform.position.x + randX, transform.position.y + randY);
        if (Time.time > timeToNextSpawn)
        {
            timeToNextSpawn = Time.time + spawnCooldown;
            for (int i = 0; i < Count; ++i)
            {
                Instantiate(Enemy, whereToSpawn, Quaternion.identity);
                //updateSpawnPos();
                randX = Random.Range(Xstart, Xend);
                randY = Random.Range(Ystart, Yend);
                whereToSpawn = new Vector3(transform.position.x + randX, transform.position.y + randY);
            }
        }
    }
    //private void updateSpawnPos()
    //{
    //    randX = Random.Range(-12f, 12);
    //    whereToSpawn = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z);
    //}
}
