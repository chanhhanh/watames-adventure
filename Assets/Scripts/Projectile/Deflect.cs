using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deflect : MonoBehaviour
{
    [SerializeField]
    int DeflectType = 0;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && collision.GetComponent<EnemyProjectileCollision>())
        {
            switch (DeflectType)
            {
                case 0:
                    Destroy(collision.gameObject);
                    break;
                case 1:
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponentInParent<Rigidbody2D>().velocity * 2;
                    Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    float rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                    collision.gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                    var pc = collision.gameObject.AddComponent<ProjectileCollision>();
                    pc.damage = 20;
                    break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 && collision.gameObject.GetComponent<EnemyProjectileCollision>())
        {
            switch (DeflectType)
            {
                case 0:
                    Destroy(collision.gameObject);
                    break;
                case 1:
                    Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    float rotation = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                    collision.gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                    break;
            }
        }
    }
}
