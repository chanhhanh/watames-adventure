using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNonDestruct : MonoBehaviour
{
    public float damage;
    public float maxCollisionCount = -1;
    private float numOfCollisions = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
            }
            numOfCollisions += 1;
            CheckCollisionCount();
        }
    }

    private void CheckCollisionCount()
    {
        if (maxCollisionCount > 0 && numOfCollisions >= maxCollisionCount)
        {
            Destroy(gameObject);
        }
    }

}
