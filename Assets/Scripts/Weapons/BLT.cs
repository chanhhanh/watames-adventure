using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLT : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float minDamage = 50, maxDamage = 50;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 3f;
    GameObject spell;

    //Audio
    public AudioClip bulletSound;

    [SerializeField]
    GameObject source;
    [SerializeField]
    List<GameObject> particles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && offCooldown && !Menu.isPaused)
        {
            SpawnBullet();
            
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
        if (bulletSound)
        {
            AudioSource.PlayClipAtPoint(bulletSound, transform.position, Menu.m_SFXVolume);
        }
        StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.2f, 0.04f));

        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        foreach(GameObject particle in particles)
        {
            particle.GetComponent<ParticleSystem>().Play();
        }

        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = Random.Range(minDamage, maxDamage);
    }
}
