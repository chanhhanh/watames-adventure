using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    public GameObject projectile;

    private float minDamage = 49;
    private float maxDamage = 52;
    private float cooldown = 2.5f;
    private bool offCooldown = true;
    public float spellLevel = 0;
    float inputHorizontal;

    //Facing
    float currentState = 1;

    const float LEFT = -1;
    const float RIGHT = 1;

    //Whip transform
    Vector2 whipPos = new Vector2(0f, 0f);
    Quaternion whipRog = Quaternion.Euler(0f, 0f, 0f);

    //Whip position offet
    private float whipOffsetY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Space) && offCooldown)
        {
            StartCoroutine(ShootEnemy());
        }
    }
    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }

    private void FixedUpdate()
    {
        if (inputHorizontal > 0)
            changeFacing(RIGHT);
        else if (inputHorizontal < 0)
        {
            changeFacing(LEFT);
        }
    }
    IEnumerator ShootEnemy()
    {
        StartCoroutine(startCooldown());

        if (spellLevel > 5) spellLevel = 5;
        float currentFacing = currentState;
        switch(spellLevel)
        {
            case 0:
                SpawnWhip(currentFacing);
                break;
            case 1:
                SpawnWhip(currentFacing);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing * -1);
                break;
            case 2:
                minDamage = 84;
                maxDamage = 87;
                SpawnWhip(currentFacing);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing * -1);
                break;
            case 3:
                minDamage = 84;
                maxDamage = 87;
                SpawnWhip(currentFacing);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing * -1);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing);
                break;
            case 4:
                minDamage = 84;
                maxDamage = 87;
                projectile.GetComponent<WhipAnimation>().sizeMultiplier = 1.1f;
                SpawnWhip(currentFacing);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing * -1);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing);
                break;
            case 5:
                minDamage = 109;
                maxDamage = 112;
                projectile.GetComponent<WhipAnimation>().sizeMultiplier = 1.1f;
                SpawnWhip(currentFacing);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing * -1);
                yield return new WaitForSeconds(0.2f);
                SpawnWhip(currentFacing);
                break;
        }

        whipOffsetY = 0f;
        //StartCoroutine(ShootEnemy());
    }

    private void changeFacing(float newState)
    {
        // Stop animations from interrupting itself
        if (currentState == newState) return;

        // Update current state
        currentState = newState;
    }

    private void SpawnWhip(float currentFacing)
    {
        if (currentFacing == RIGHT)
        {
            whipPos = new Vector2(transform.position.x + 1.5f, transform.position.y + whipOffsetY);
            whipRog = Quaternion.Euler(180f, 0f, 0f);
        }
        else if (currentFacing == LEFT)
        {
            whipPos = new Vector2(transform.position.x - 1.5f, transform.position.y + whipOffsetY);
            whipRog = Quaternion.Euler(0f, -180f, 0f);
        }

        GameObject spell = Instantiate(projectile, whipPos, Quaternion.identity);
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
        spell.GetComponent<Transform>().rotation = whipRog;
        whipOffsetY += 0.5f;
    }
}
