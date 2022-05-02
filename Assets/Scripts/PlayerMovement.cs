using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    const string PLAYER_UP = "Idle_up";
    const string PLAYER_DOWN = "Idle_down";
    const string PLAYER_LEFT = "Idle_left";
    const string PLAYER_RIGHT = "Idle_right";

    const string PLAYER_DOWNLEFT = "Idle_downleft";
    const string PLAYER_DOWNRIGHT = "Idle_downright";
    const string PLAYER_LEFTUP = "Idle_upleft";
    const string PLAYER_RIGHTUP = "Idle_upright";


    const string PLAYER_WALK_UP = "Walk_up";
    const string PLAYER_WALK_DOWN = "Walk_down";
    const string PLAYER_WALK_LEFT = "Walk_left";
    const string PLAYER_WALK_RIGHT = "Walk_right";

    const string PLAYER_WALK_DOWNLEFT = "Walk_downleft";
    const string PLAYER_WALK_DOWNRIGHT = "Walk_downright";
    const string PLAYER_WALK_LEFTUP = "Walk_leftup";
    const string PLAYER_WALK_RIGHTUP = "Walk_rightup";

    public float DashForce = 3000f;
    private bool CanIDash = true;
    public float cooldown = 1.5f;
    //Start is called at the first frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //if (Input.GetKeyDown(KeyCode.LeftShift) && CanIDash)
        //{
        //    StartCoroutine(Dash());
        //    Debug.Log("shift pressed");
        //}

    }
    //IEnumerator Dash()
    //{
    //    float dashSpeedX = (inputHorizontal * moveSpeed * 30f) * GetComponent<Rigidbody2D>().mass / Time.fixedDeltaTime;
    //    float dashSpeedY = (inputVertical * moveSpeed * 30f) * GetComponent<Rigidbody2D>().mass / Time.fixedDeltaTime;
    //    rb.AddForce(new Vector2 (dashSpeedX, dashSpeedY));
    //    StartCoroutine(CountCooldown());
    //    yield return new WaitForSeconds(1f);
    //}

    IEnumerator CountCooldown()
    {
        CanIDash = false;
        yield return new WaitForSeconds(cooldown);
        CanIDash = true;
    }
    void FixedUpdate()
    {
        if(inputHorizontal != 0 || inputVertical != 0)
        {
            if(inputHorizontal != 0 && inputVertical != 0)
            {
                rb.velocity = new Vector2(inputHorizontal * moveSpeed * speedLimiter, inputVertical * moveSpeed * speedLimiter);
            }
            rb.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);


            if (inputHorizontal > 0)
            {
                if (inputVertical > 0)
                    changeAnimationState(PLAYER_WALK_RIGHTUP);
                else if (inputVertical < 0)
                    changeAnimationState(PLAYER_WALK_DOWNRIGHT);
                else changeAnimationState(PLAYER_WALK_RIGHT);
            }
            else if (inputHorizontal < 0)
            {
                if (inputVertical > 0)
                    changeAnimationState(PLAYER_WALK_LEFTUP);
                else if (inputVertical < 0)
                    changeAnimationState(PLAYER_WALK_DOWNLEFT);
                else changeAnimationState(PLAYER_WALK_LEFT);
            }
            else if (inputVertical > 0)
            {
                changeAnimationState(PLAYER_WALK_UP);
            }
            else if (inputVertical < 0)
            {
                changeAnimationState(PLAYER_WALK_DOWN);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
            switch (currentState)
            {
                case PLAYER_WALK_DOWNLEFT:
                    changeAnimationState(PLAYER_DOWNLEFT);
                    return;
                case PLAYER_WALK_DOWNRIGHT:
                    changeAnimationState(PLAYER_DOWNRIGHT);
                    return;
                case PLAYER_WALK_LEFTUP:
                    changeAnimationState(PLAYER_LEFTUP);
                    return;
                case PLAYER_WALK_RIGHTUP:
                    changeAnimationState(PLAYER_RIGHTUP);
                    return;
                case PLAYER_WALK_DOWN:
                    changeAnimationState(PLAYER_DOWN);
                    return;
                case PLAYER_WALK_UP:
                    changeAnimationState(PLAYER_UP);
                    return;
                case PLAYER_WALK_LEFT:
                    changeAnimationState(PLAYER_LEFT);
                    return;
                case PLAYER_WALK_RIGHT:
                    changeAnimationState(PLAYER_RIGHT);
                    return;
            }
        }
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
