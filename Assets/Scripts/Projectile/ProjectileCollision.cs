using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null && collision.GetComponent<EnemyMovement>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
                collision.GetComponent<EnemyMovement>().PushBack(GetComponent<Rigidbody2D>().velocity);
            }
            if (collision.tag == "Debris" || collision.tag =="Enemy") Destroy(gameObject);
        }
    }
}
