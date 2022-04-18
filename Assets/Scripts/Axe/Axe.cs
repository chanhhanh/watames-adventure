using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public GameObject projectile;
    GameObject spell;

    private float minDamage = 18;
    private float maxDamage = 21;
    private float cooldown = 3.5f;
    public float force = 45f;
    public float spellLevel = 0;
    float inputHorizontal;

    //Facing
    float currentState = 1;

    const float LEFT = -1;
    const float RIGHT = 1;

    //Axe force
    Vector2 axeHorForce = new Vector2(0f, 0f);
    public float torque = 360f;
  

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
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
        if (spellLevel > 5) spellLevel = 5;
        yield return new WaitForSeconds(cooldown);
        float currentFacing = currentState;
        float oppositeFacing = currentFacing * -1;
        switch(spellLevel)
        {
            case 0:
                SpawnAxe();
                break;
            case 1:
                SpawnAxe();
                SpawnAxeWithFacing(currentFacing);
                break;
            case 2:
                minDamage = 38;
                maxDamage = 41;
                SpawnAxe();
                SpawnAxeWithFacing(currentFacing);
                break;
            case 3:
                minDamage = 38;
                maxDamage = 41;
                projectile.GetComponent<CollisionNonDestruct>().maxCollisionCount = 5;
                SpawnAxe();
                SpawnAxeWithFacing(currentFacing);
                break;
            case 4:
                minDamage = 38;
                maxDamage = 41;
                projectile.GetComponent<CollisionNonDestruct>().maxCollisionCount = 5;
                SpawnAxe();
                SpawnAxeWithFacing(currentFacing);
                SpawnAxeWithFacing(oppositeFacing);
                break;
            case 5:
                minDamage = 58;
                maxDamage = 61;
                projectile.GetComponent<CollisionNonDestruct>().maxCollisionCount = 5;
                SpawnAxe();
                SpawnAxeWithFacing(currentFacing);
                SpawnAxeWithFacing(oppositeFacing);
                break;
        }
        StartCoroutine(ShootEnemy());
    }

    private void changeFacing(float newState)
    {
        if (currentState == newState) return;

        // Update current state
        currentState = newState;
    }

    private void SpawnAxe()
    {
        axeHorForce = new Vector2(transform.position.x, transform.position.y + force);
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
        spell.GetComponent<Rigidbody2D>().AddForce(axeHorForce);
        spell.GetComponent<Rigidbody2D>().AddTorque(torque);
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
    }
    private void SpawnAxeWithFacing(float currentFacing)
    {

        if (currentFacing == RIGHT)
        {
            axeHorForce = new Vector2(transform.position.x + 10f, transform.position.y + force);
        }
        else if (currentFacing == LEFT)
        {
            axeHorForce = new Vector2(transform.position.x - 10f, transform.position.y + force);
        }

        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
        spell.GetComponent<Rigidbody2D>().AddForce(axeHorForce);
        spell.GetComponent<Rigidbody2D>().AddTorque(torque);
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
    }
}
