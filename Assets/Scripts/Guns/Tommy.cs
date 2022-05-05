using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tommy : MonoBehaviour
{
    public GameObject projectile;

    private float minDamage = 5;
    private float maxDamage = 7;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 0.05f;
    public float spellLevel = 0;
    GameObject spell;

    //Audio
    public AudioSource aus;

    public AudioClip bulletSound;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
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
        float recoil = UnityEngine.Random.Range(0.8f, 1.2f);

        spell = Instantiate(projectile, transform.position, transform.rotation);
        spell.GetComponent<Rigidbody2D>().velocity = new Vector2(recoil, recoil);
        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
