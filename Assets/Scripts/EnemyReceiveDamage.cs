using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public GameObject drop;
    [SerializeField]
    SpriteRenderer sprite;
    [Header("Boss Settings")]
    [SerializeField]
    bool isBoss = false;
    public string bossName;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (isBoss)
            PlayerStats.Instance.InitBoss(bossName, gameObject);
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage());
        if(isBoss) StartCoroutine(PlayerStats.Instance.UpdateBossHealth(health, maxHealth));
        CheckDeath();
    }
    private void OnDestroy()
    {
        if (!isBoss)
        {
            EnemySpawner.instance.maxSpawn--;
        }
        else PlayerStats.Instance.HideBossBar();
        SpawnDrop();
    }
    IEnumerator FlashDamage()
    {
        if(!sprite)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        } 
            
    }

    //checkOverheal is not really useful for now
    private void checkOverheal()
    {
        if (health > maxHealth) health = maxHealth;
    }

    private void CheckDeath()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void SpawnDrop()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}
