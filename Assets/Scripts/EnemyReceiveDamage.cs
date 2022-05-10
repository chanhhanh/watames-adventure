using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public GameObject drop;
    [SerializeField]
    GameObject previousDamageSrc;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage());
        CheckDeath();
    }
    private void OnDestroy()
    {
        EnemySpawner.instance.maxSpawn--;
        SpawnDrop();
    }
    IEnumerator FlashDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
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
