using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public GameObject drop;
    [SerializeField]
    GameObject thrownEnemy;

    [SerializeField]
    SpriteRenderer sprite;
    [Header("Boss Settings")]
    [SerializeField]
    bool isBoss = false;
    [SerializeField]
    public string bossName;
    [SerializeField]
    float previousDamage;

    Vector2 projectileDirection;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (isBoss)
            PlayerStats.Instance.InitBoss(bossName, gameObject);
    }

    public void DealDamage(float damage, Vector2 dir)
    {
        health -= damage;
        previousDamage = damage;
        projectileDirection = dir;
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
    private void CheckDeath()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (previousDamage >= 45 && thrownEnemy)
        {
            GameObject enemy = Instantiate(thrownEnemy, transform.position, Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            enemy.GetComponent<Rigidbody2D>().AddForce(previousDamage * projectileDirection.normalized);
            enemy.GetComponent<Rigidbody2D>().AddTorque(360f);
        }
        Destroy(gameObject);
    }
    private void SpawnDrop()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}
