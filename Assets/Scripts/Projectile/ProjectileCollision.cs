using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public float damage;
    [SerializeField]
    GameObject particle;
    [SerializeField]

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage, GetComponent<Rigidbody2D>().velocity);
            }
            if (collision.tag == "Debris" || collision.tag =="Enemy") Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (particle && !Menu.isReloading)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
