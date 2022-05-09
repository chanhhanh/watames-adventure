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
    [SerializeField]
    GameObject source;

    [SerializeField]
    float projectileSpread = 1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
        {
            SpawnBullet();
            if (aus && bulletSound)
            {
                AudioSource.PlayClipAtPoint(bulletSound, transform.position);
            }
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.1f, 0.02f));
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
        float recoil = UnityEngine.Random.Range(-projectileSpread/2, projectileSpread/2);

        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        source.GetComponent<ParticleSystem>().Play();

        spell.GetComponent<Rigidbody2D>().velocity = new Vector2(recoil, recoil).normalized;
        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }
}
