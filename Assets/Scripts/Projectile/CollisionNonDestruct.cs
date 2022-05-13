using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNonDestruct : MonoBehaviour
{
    public float damage;
    public float maxCollisionCount = -1;
    private float numOfCollisions = 0;
    [SerializeField]
    GameObject particle;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage, GetComponent<Rigidbody2D>().velocity);
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
    private void OnDestroy()
    {
        if(particle && !Menu.isReloading)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
