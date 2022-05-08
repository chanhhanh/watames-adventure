using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shotgun : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    int numOfProjectiles = 6;
    [SerializeField]
    float projectileSpread = 60f;

    private float minDamage = 5;
    private float maxDamage = 7;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 1f;
    public float spellLevel = 0;
    GameObject spell;

    //Audio
    public AudioSource aus;

    public AudioClip bulletSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && offCooldown)
        {
            SpawnBullet();
            if (aus && bulletSound)
            {
                aus.PlayOneShot(bulletSound);
            }
            StartCoroutine(startCooldown());
        }
    }

    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
 
    private void SpawnBullet()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float facingRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation + projectileSpread / 2;
        float angleIncrease = projectileSpread / ((float)numOfProjectiles - 1f);
        for (int i=0; i< numOfProjectiles; ++i)
        {
            float tempRot = startRotation - angleIncrease * i;
            spell = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,tempRot));
           
            Vector2 trajectory = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad) * projectileForce, Mathf.Sin(tempRot * Mathf.Deg2Rad) * projectileForce);
            spell.GetComponent<Rigidbody2D>().velocity = trajectory;
            spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        }

    }
}
