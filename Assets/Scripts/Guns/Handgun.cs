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
    GameObject spell;

    //Audio
    public AudioClip bulletSound;
 
    [SerializeField]
    GameObject source;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Menu.isPaused)
        {
            SpawnBullet();
            if(bulletSound)
            {
                //aus.PlayOneShot(bulletSound);
                AudioSource.PlayClipAtPoint(bulletSound, transform.position);
            }
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.1f, 0.02f));
        }
    }

    private void SpawnBullet()
    {
        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        source.GetComponent<ParticleSystem>().Play();

        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
