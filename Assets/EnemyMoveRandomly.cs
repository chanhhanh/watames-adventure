using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandomly : MonoBehaviour
{
    public float moveSpeed = 3f;
    Vector2 randomPos;
    Animator animator;


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


    public float forceMultiplier = 100f;
    IEnumerator MoveRandomly()
    {
        int[] moveValue = { -3, 0, 3 };
        int randIndexX = Random.Range(0, moveValue.Length);
        int randIndexY = Random.Range(0, moveValue.Length);
        randomPos = new Vector2(moveValue[randIndexX], moveValue[randIndexY]);
        Debug.Log(randomPos);
        GetComponent<Rigidbody2D>().velocity = randomPos;
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveRandomly());
    }
}
