using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    private Transform player;

    private Vector3 previousPosition;
    private Vector3 currentMovementDirection;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }
        if (player)
            Move();
        if (currentMovementDirection.x > 0)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180f, 0);
        }
        else GetComponent<Transform>().rotation = Quaternion.Euler(0, 0f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.DealDamage(damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.DealDamage(damage);
        }
    }

    private void Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 direction = (player.position - transform.position);
        float DirX = -1f, DirY = -1f;
        if (direction.x > 0) DirX = 1f;
        if (direction.y > 0) DirY = 1f;
        Vector2 Dir = new Vector2(DirX, DirY);
        
        rb.velocity = Dir * moveSpeed;
        //if (direction.magnitude < 1) rb.velocity = Vector2.zero;
        //GetComponent<Rigidbody2D>().MovePosition(transform.position + (moveSpeed * Time.fixedDeltaTime * direction));

        //rb.AddForce();

    }
}
