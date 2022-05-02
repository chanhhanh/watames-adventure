using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject bat;
    public GameObject ghost;
    public Text enemyCounter;
    public Text timer;

    Vector3 whereToSpawn;
    public float spawnCooldown = 10f;
    public float timeToNextSpawn = 1f;
    int countTimer = 60;

    bool allEnemiesSpawned = false;
    int currentWave = 1;
    public int enemyCount;
    int[] WAVES_COUNT = { 0, 7, 14, 21, 36, 48, 64 };
    void Start()
    {
        timer.enabled = false;
        enemyCount = WAVES_COUNT[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!allEnemiesSpawned) Spawn();
        if (allEnemiesSpawned) checkForWaveFinish();
        enemyCounter.text = enemyCount.ToString();
    }

    private void Spawn()
    {

        if (Time.time > timeToNextSpawn)
        {
            timeToNextSpawn = Time.time + spawnCooldown;
            switch (currentWave)
            {
                case 1:
                        StartCoroutine(Wave_1());
                        StopCoroutine(Wave_1());
                    break;
            }
            ChangeWave();
        }
    }

    private IEnumerator Wave_1()
    {
        whereToSpawn = FindRandomSpawnPoint().transform.position;
        for (int i = 0; i < 3; ++i)
        {
            Instantiate(bat, whereToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        whereToSpawn = FindRandomSpawnPoint().transform.position;

        yield return new WaitForSeconds(spawnCooldown);
        for (int i = 0; i < 3; ++i)
        {
            Instantiate(bat, whereToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        whereToSpawn = FindRandomSpawnPoint().transform.position;

        yield return new WaitForSeconds(spawnCooldown);
        for (int i = 0; i < 1; ++i)
        {
            Instantiate(ghost, whereToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
    public GameObject FindRandomSpawnPoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int index = Random.Range(0, gos.Length);
        GameObject spawnPoint = gos[index];
        return spawnPoint;
    }
    private void ChangeWave()
    {
        currentWave += 1;
        Debug.Log(currentWave);
        allEnemiesSpawned = true;
    }

    private void checkForWaveFinish()
    {
        if (enemyCount == 0 && enemyCounter.enabled)
        {
            enemyCounter.enabled = false;
            timer.enabled = true;
            StartCoroutine(CountdownTime());
        }
    }

    IEnumerator CountdownTime()
    {
        int localTimer = countTimer;
        timer.text = localTimer.ToString();
        for (int i = 0; i < countTimer; ++i)
        {
            localTimer -= 1;
            timer.text = localTimer.ToString();
            yield return new WaitForSeconds(1f);
        }
        timer.enabled = false;
        enemyCount = WAVES_COUNT[currentWave];
        enemyCounter.enabled = true;
        allEnemiesSpawned = false;
    }
}
