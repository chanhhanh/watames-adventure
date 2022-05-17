using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Player
    public float moveSpeed;
    public float speedLimiter = 0.7f;
    float inputVertical;
    float inputHorizontal;

    //Animations and states
    Animator animator;
    string currentState;

    const string IDLE = "Watane_Idle";
    const string RUN = "Watame_Run";

    public Joystick joystick;

    //Start is called at the first frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.m_gamepad)
        {
            inputHorizontal = joystick.Horizontal;
            inputVertical = joystick.Vertical;
        }
        else
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
        }
    }
  
    void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float y = 0f;
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                rb.velocity = new Vector2(inputHorizontal * moveSpeed * speedLimiter, inputVertical * moveSpeed * speedLimiter);
            }
            rb.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);
            changeAnimationState(RUN);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
            changeAnimationState(IDLE);
        }
        if (direction.x < 0)
        {
            y = -180f;
        }
        Quaternion target = Quaternion.Euler(0f, y, 0f);
        GetComponent<Transform>().rotation = target;
    }

    //animation state changer
    private void changeAnimationState(string newState)
    {
        // Stop animations from interrupting itself
        if (currentState == newState) return;
        // Play new anim
        animator.Play(newState);
        // Update current state
        currentState = newState;
    }


}
