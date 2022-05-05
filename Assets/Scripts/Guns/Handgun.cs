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
    private bool offCooldown = true;
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

    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
    IEnumerator ShootEnemy()
    {
        StartCoroutine(startCooldown());
        //if (!this.enabled) StopAllCoroutines() else;

        if (spellLevel > 5) spellLevel = 5;
                switch (spellLevel)
                {
                    case 0:
                    SpawnBullet();
                    break;
                    case 1:
                    for(int i =0; i< 2; ++i)
                    {
                        SpawnBullet();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 2:
                    for (int i = 0; i < 2; ++i)
                    {
                        SpawnBullet();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 3:
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnBullet();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 4:
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnBullet();
                        yield return new WaitForSeconds(0.2f);
                      
                    }
                    break;
                    case 5:
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 4; ++i)
                    {
                        SpawnBullet();
                        yield return new WaitForSeconds(0.2f);
                       
                    }
                    break;
                }

        //yield return new WaitForSeconds(cooldown);

        //StartCoroutine(ShootEnemy());
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void SpawnBullet()
    {
        spell = Instantiate(projectile, transform.position, transform.rotation);
        
        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
