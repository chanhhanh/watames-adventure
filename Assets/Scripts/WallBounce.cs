using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        lastVelocity = rb.velocity;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //        var speed = lastVelocity.magnitude;
    //        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
    //        rb.velocity = direction * Mathf.Max(speed, 0f);
        
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            var cp = collision.ClosestPoint(transform.position);
            var speed = lastVelocity.magnitude;
            Debug.Log(cp);
            Debug.Log(speed);
            var direction = Vector3.Reflect(lastVelocity.normalized, cp);
            rb.velocity = direction.normalized * speed;
        }

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        collision.GetContacts(contacts);
    //        Debug.Log(contacts[0].normal);
    //        var speed = lastVelocity.magnitude;
    //        var direction = Vector3.Reflect(lastVelocity.normalized, contacts[0].normal);
    //        rb.velocity = direction * Mathf.Max(speed, 0f);
    //    }
    //}

    //{
    //    var speed = lastVelocity.magnitude;
    //    var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
    //}
}
