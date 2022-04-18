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

    // Update is called once per frame
    void Update()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }
        if(player)
            Move();
    }

    void FixedUpdate()
    {
        if (currentMovementDirection.x > 0)
        {
            animator.Play("Move_left");
        }
        else animator.Play("Move_right");
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
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
