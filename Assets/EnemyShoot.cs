using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    GameObject player;
    public GameObject projectile;
    [SerializeField]
    private float bulletCount = 5, spacing = 0.3f, minDamage = 15f, maxDamage = 20f, projectileForce = 5f;
    [SerializeField]
    bool fromSource = false;
    [SerializeField]
    AudioClip auc;
    [SerializeField]
    ParticleSystem particle;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        if (player)
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                ShootProjectile();
                yield return new WaitForSeconds(spacing);
            }
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(StartShooting());
    }

    private void ShootProjectile()
    {
        if (auc) AudioSource.PlayClipAtPoint(auc, transform.position);
        if (particle) particle.Play();
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);

        switch (fromSource)
        {
            case false:

                Vector3 direction = (player.transform.position - transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                spell.GetComponent<Rigidbody2D>().rotation = angle;
                spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                break;
            case true:
                spell.GetComponent<Rigidbody2D>().velocity = transform.right * projectileForce;
                break;
        }
        spell.GetComponent<EnemyProjectileCollision>().damage = Random.Range(minDamage, maxDamage);

    }
}
