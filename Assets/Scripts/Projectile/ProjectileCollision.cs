using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
