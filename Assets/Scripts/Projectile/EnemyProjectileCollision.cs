using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    public float damage;
    [SerializeField]
    GameObject particle;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            GameManager.Instance.DealDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Debris"))
        {
            Destroy(gameObject);
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
