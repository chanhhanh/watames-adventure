using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicWand : MonoBehaviour
{
    public GameObject projectile;
    
    private float minDamage = 9;
    private float maxDamage = 12;
    public float projectileForce = 6f;
    private bool offCooldown = true;
    public float cooldown = 0.2f;
    GameObject spell;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
        {
          ShootEnemy();
        }
    }

    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
    void ShootEnemy()
    {
        StartCoroutine(spinItem());
        SpawnMagicMissile();
        StartCoroutine(startCooldown());
    }
   
    IEnumerator spinItem()
    {
        float z = 0f;
        Quaternion target = Quaternion.Euler(0f, 0f, z);
        while (z >= -360f)
        {
            GetComponent<Transform>().localRotation = target;
            yield return new WaitForSeconds(0.1f);
            z--;
        }
        target = Quaternion.Euler(0f, 0f, 0f);
        GetComponent<Transform>().rotation = target;
    }
    private void SpawnMagicMissile()
    {
        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }

   
}
