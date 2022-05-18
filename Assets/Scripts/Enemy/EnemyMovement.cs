using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float damage;
    private Transform player;

    private Vector3 previousPosition;
    private Vector3 currentMovementDirection;
    private NavMeshAgent agent;
    [SerializeField]
    float moveDuration = 0f, stopDuration = 0f;

    private bool isStopped = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        StartCoroutine(StopMoving());
    }

    void FixedUpdate()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }

        if (currentMovementDirection.x > 0)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180f, 0);
        }
        else GetComponent<Transform>().rotation = Quaternion.Euler(0, 0f, 0);
        if(!isStopped && player) agent.SetDestination(player.position);
    }

    IEnumerator StopMoving()
    {
        if(moveDuration != 0 && stopDuration !=0)
        {
            yield return new WaitForSeconds(moveDuration);
            isStopped = true;
            yield return new WaitForSeconds(stopDuration);
            isStopped = false;
            StartCoroutine(StopMoving());
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DealDamage(damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DealDamage(damage);
        }
    }
}
