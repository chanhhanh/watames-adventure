using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicWand : MonoBehaviour
{
    public GameObject projectile;
    
    private float minDamage = 9;
    private float maxDamage = 12;
    public float projectileForce = 5;
    private float cooldown = 1.5f;
    public float spellLevel = 0;
    private Vector2 direction;
    private float angle;
    GameObject spell;

    void Start()
    {
        StartCoroutine(ShootEnemy());
    }

    IEnumerator ShootEnemy()
    {

        //if (!this.enabled) StopAllCoroutines() else;

        if (spellLevel > 5) spellLevel = 5;
        yield return new WaitForSeconds(cooldown);
            if (FindClosestEnemy())
            {
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
                    cooldown = 1.3f;
                    for (int i = 0; i < 2; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                    }
                    break;
                    case 3:
                    cooldown = 1.3f;
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                    }
                    break;
                    case 4:
                    cooldown = 1.3f;
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 3; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                    }
                    break;
                    case 5:
                    cooldown = 1.3f;
                    minDamage = 19;
                    maxDamage = 21;
                    for (int i = 0; i < 4; ++i)
                    {
                        SpawnMagicMissile();
                        yield return new WaitForSeconds(0.2f);
                    }
                    break;
                }
            }
        StartCoroutine(ShootEnemy());
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

        direction = (FindClosestEnemy().transform.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
