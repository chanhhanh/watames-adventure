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

    private float minDamage = 15;
    private float maxDamage = 17;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 1f;
    GameObject spell;

    //Audio
    public AudioClip bulletSound;
    [SerializeField]
    GameObject source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && offCooldown && !Menu.isPaused)
        {
            SpawnBullet();
            if (bulletSound)
            {
                AudioSource.PlayClipAtPoint(bulletSound, transform.position);
            }
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.1f, 0.02f));
            StartCoroutine(startCooldown());
        }
    }

    IEnumerator startCooldown()
    {
        StartCoroutine(PlayerStats.Instance.VisualizeCooldown(cooldown));
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
 
    private void SpawnBullet()
    {
        Vector2 mousePos = PlayerStats.Instance.m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float facingRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation + projectileSpread / 2;
        float angleIncrease = projectileSpread / (numOfProjectiles - 1f);
        for (int i=0; i< numOfProjectiles; ++i)
        {
            float tempRot = startRotation - angleIncrease * i;
            spell = Instantiate(projectile, source.transform.position, Quaternion.Euler(0,0,tempRot));
            source.GetComponent<ParticleSystem>().Play();

            Vector2 trajectory = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad) * projectileForce, Mathf.Sin(tempRot * Mathf.Deg2Rad) * projectileForce);
            spell.GetComponent<Rigidbody2D>().velocity = trajectory;
            spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        }

    }
}
