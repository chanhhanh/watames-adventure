using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicWand : MonoBehaviour
{
    public GameObject projectile;
    
    private float minDamage = 9;
    private float maxDamage = 12;
    public float projectileForce = 14f;
    private bool offCooldown = true;
    public float cooldown = 0.2f;
    GameObject spell;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown && !Menu.isPaused)
        {
            ShootEnemy();
            SwingWeapon();
        }
    }

    void SwingWeapon()
    {
        animator.Play("Weapon_Swing_Right");
    }
    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
    void ShootEnemy()
    {
        SpawnMagicMissile();
        StartCoroutine(startCooldown());
    }
   
    private void SpawnMagicMissile()
    {
        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().AddForce(transform.parent.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }

   
}
