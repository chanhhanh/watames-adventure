using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Handgun : MonoBehaviour
{
    public GameObject projectile;

    private float minDamage = 5;
    private float maxDamage = 7;
    public float projectileForce = 15f;
    public float cooldown = 0.5f;
    public float spellLevel = 0;
    GameObject spell;

    //Audio
    public AudioSource aus;

    public AudioClip bulletSound;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnBullet();
            if(aus && bulletSound)
            {
                aus.PlayOneShot(bulletSound);
            }
        }
    }

    private void SpawnBullet()
    {
        spell = Instantiate(projectile, transform.position, transform.rotation);
        
        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
