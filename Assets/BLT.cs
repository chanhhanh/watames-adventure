using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLT : MonoBehaviour
{
    public GameObject projectile;

    private float minDamage = 50;
    private float maxDamage = 50;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 3f;
    public float spellLevel = 0;
    GameObject spell;

    //Audio
    public AudioClip bulletSound;

    [SerializeField]
    GameObject source;
    [SerializeField]
    List<GameObject> particles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && offCooldown)
        {
            SpawnBullet();
            if (bulletSound)
            {
                //aus.PlayOneShot(bulletSound);
                AudioSource.PlayClipAtPoint(bulletSound, transform.position);
            }
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.2f, 0.04f));
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
        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        foreach(GameObject particle in particles)
        {
            particle.GetComponent<ParticleSystem>().Play();
        }

        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = Random.Range(minDamage, maxDamage);
    }
}
