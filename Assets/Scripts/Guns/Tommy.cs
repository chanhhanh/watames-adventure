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
    public AudioClip bulletSound;
    [SerializeField]
    GameObject source;

    [SerializeField]
    float projectileSpread = 1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
        {
            StartCoroutine(startCooldown());
            SpawnBullet();
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.1f, 0.02f));
        }
    }

    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }


    [SerializeField]
    float knockbackMultiplier = 0.1f;
    private void SpawnBullet()
    {
        AudioSource.PlayClipAtPoint(bulletSound, transform.position, 1f);

        float recoil = UnityEngine.Random.Range(-projectileSpread/2, projectileSpread/2);

        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        source.GetComponent<ParticleSystem>().Play();

        Vector2 r = new Vector2(transform.right.x, transform.right.y + recoil).normalized;
        spell.GetComponent<Rigidbody2D>().AddForce(r * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        Vector2 knockback = spell.GetComponent<Rigidbody2D>().velocity.normalized * -1 * knockbackMultiplier;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback);
    }
}
