using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    public float damage;
    private GameObject gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameManager.GetComponent<PlayerStats>().DealDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Debris")
        {
            Destroy(gameObject);
        }
    }
}
