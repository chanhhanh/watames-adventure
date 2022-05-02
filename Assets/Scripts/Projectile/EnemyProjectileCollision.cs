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
            if (collision.tag == "Debris" || collision.name =="Player") Destroy(gameObject);
        }
    }
}
