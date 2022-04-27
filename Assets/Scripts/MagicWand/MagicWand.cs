using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicWand : MonoBehaviour
{
    public GameObject projectile;
    
    private float minDamage = 9;
    private float maxDamage = 12;
    public float projectileForce = 7f;
    private bool offCooldown = true;
    public float cooldown = 0.5f;
    public float spellLevel = 0;
    private Vector2 direction;
    private float angle;
    GameObject spell;

    void Start()
    {
        //StartCoroutine(ShootEnemy());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
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
    IEnumerator ShootEnemy()
    {
        StartCoroutine(startCooldown());
        //if (!this.enabled) StopAllCoroutines() else;

        if (spellLevel > 5) spellLevel = 5;
                switch (spellLevel)
                {
                    case 0:
                    SpawnMagicMissile();
                    break;
                    case 1:
                    for(int i =0; i< 2; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 2:
                    for (int i = 0; i < 2; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 3:
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                        
                    }
                    break;
                    case 4:
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                      
                    }
                    break;
                    case 5:
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 4; ++i)
                    {
                        SpawnMagicMissile();
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

    private void SpawnMagicMissile()
    {
        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }

    private void Homing()
    {
        //if(FindClosestEnemy())
        //{
        //    direction = (FindClosestEnemy().transform.position - transform.position).normalized;
        //    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    spell.GetComponent<Rigidbody2D>().rotation = angle;
        //    spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce * 3f;
        //}
    }
}
