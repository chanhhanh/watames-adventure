using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandomly : MonoBehaviour
{
    Vector2 randomPos;
    Animator animator;
    [SerializeField]
    float travelTime = 1f, interval = 1f, moveSpeed = 3f, damage = 0f;

    private Vector3 previousPosition;
    private Vector3 currentMovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRandomly());
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPosition != transform.position)
        {
            currentMovementDirection = (previousPosition - transform.position).normalized;
            previousPosition = transform.position;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            PlayerStats.Instance.DealDamage(damage);
        }
    }
    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        if (currentMovementDirection.x > 0)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180f, 0);
        }
        else GetComponent<Transform>().rotation = Quaternion.Euler(0, 0f, 0);
    }

    IEnumerator MoveRandomly()
    {
        float[] moveValue = { -moveSpeed, 0, moveSpeed };
        int randIndexX = Random.Range(0, moveValue.Length);
        int randIndexY = Random.Range(0, moveValue.Length);
        randomPos = new Vector2(moveValue[randIndexX], moveValue[randIndexY]);
        GetComponent<Rigidbody2D>().velocity = randomPos;
        yield return new WaitForSeconds(travelTime);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(interval);
        StartCoroutine(MoveRandomly());
    }
}
