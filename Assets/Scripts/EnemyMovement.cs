using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    private Transform player;
    Animator animator;
    private GameObject gameManager;

    private Vector3 previousPosition;
    private Vector3 currentMovementDirection;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    void Update()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }
        if (player)
            Move();
    }

    void FixedUpdate()
    {
        if (currentMovementDirection.x > 0)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180f, 0);
        }
        else GetComponent<Transform>().rotation = Quaternion.Euler(0, 0f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameManager.GetComponent<PlayerStats>().DealDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameManager.GetComponent<PlayerStats>().DealDamage(damage);
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
