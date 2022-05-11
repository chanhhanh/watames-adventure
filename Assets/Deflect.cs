using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deflect : MonoBehaviour
{
    [SerializeField]
    int DeflectType = 0;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
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
