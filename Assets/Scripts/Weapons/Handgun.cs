using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Handgun : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float minDamage = 5, maxDamage = 7;
    public float projectileForce = 15f;
    public float cooldown = 0.5f;
    GameObject bullet;

    //Audio
    public AudioClip bulletSound;
 
    [SerializeField]
    GameObject source;
    [SerializeField]
    List<GameObject> particles;
    [Header("Camera Shake Settings")]
    public float duration = 0.2f;
    public float magnitude = 0.1f;

    private void Update()
    {
        if (!Menu.m_gamepad)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !Menu.isPaused)
            {
                SpawnBullet();
            }
        }
    }

    private void SpawnBullet()
    {
        if (bulletSound)
        {
            AudioSource.PlayClipAtPoint(bulletSound, transform.position, Menu.m_SFXVolume);
        }
        StartCoroutine(PlayerCamera.Instance.ShakeOnce(magnitude, duration));
        bullet = Instantiate(projectile, source.transform.position, transform.rotation);

        foreach (GameObject particle in particles)
        {
            particle.GetComponent<ParticleSystem>().Play();
        }

        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        bullet.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
