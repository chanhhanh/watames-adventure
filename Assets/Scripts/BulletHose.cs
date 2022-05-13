using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHose : MonoBehaviour
{
    GameObject player;
    public GameObject projectile;
    [SerializeField]
    private float bulletCount = 100, spacing = 0.1f, cooldown = 5f, projectileForce = 7f, angleOffset = 7f;
    [SerializeField]
    bool inverseShooting = false;
    [SerializeField]
    AudioClip auc;
    [SerializeField]
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        float facingRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation;
        float angleIncrease = 0f;

        float tempRot = startRotation + angleIncrease;
        int i = 0;
        while(i < bulletCount)
        {
            ShootProjectile(tempRot);
            if (inverseShooting) ShootProjectile(tempRot, -1);
            angleIncrease+=angleOffset;
            tempRot = startRotation + angleIncrease;
            i++;
            yield return new WaitForSeconds(spacing);
        }
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(StartShooting());
    }

    private void ShootProjectile(float tempRot, float i = 1)
    {
        if (auc) AudioSource.PlayClipAtPoint(auc, transform.position, Menu.m_SFXVolume);
        if (particle) particle.Play();
        float inverseRot = 0f;
        if (i == -1) inverseRot = 180f;
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, tempRot + inverseRot));

        Vector2 trajectory = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad) * projectileForce * i, Mathf.Sin(tempRot * Mathf.Deg2Rad) * projectileForce * i);
        spell.GetComponent<Rigidbody2D>().velocity = trajectory;
        spell.GetComponent<EnemyProjectileCollision>().damage = Random.Range(15, 20);
    }
}
