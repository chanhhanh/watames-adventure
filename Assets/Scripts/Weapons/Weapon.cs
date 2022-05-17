using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float minDamage = 50, maxDamage = 50, projectileSpread = 0.1f;
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
    [Header("Camera Shake Settings")]
    public float duration = 0.02f;
    public float magnitude = 0.1f;

    private float inputHorizontal;
    private float inputVertical;
    private void Update()
    {
        if (!Menu.m_gamepad)
        {
            if (Input.GetKey(KeyCode.Mouse0) && offCooldown && !Menu.isPaused)
            {
                SpawnBullet();

                StartCoroutine(StartCooldown());
            }
        }
        else 
        {
            inputHorizontal = Menu.instance.m_rightThumbstick.GetComponent<FixedJoystick>().Horizontal;
            inputVertical = Menu.instance.m_rightThumbstick.GetComponent<FixedJoystick>().Vertical;
            if (inputHorizontal != 0 && inputVertical != 0 && offCooldown && !Menu.isPaused)
            {
                    SpawnBullet();
                    StartCoroutine(StartCooldown());
            }
        }
    }

    IEnumerator StartCooldown()
    {
        if (cooldown > 0.99f) StartCoroutine(PlayerStats.Instance.VisualizeCooldown(cooldown));
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
        StartCoroutine(PlayerCamera.Instance.ShakeOnce(magnitude, duration));

        spell = Instantiate(projectile, source.transform.position, transform.rotation);

        foreach(GameObject particle in particles)
        {
            particle.GetComponent<ParticleSystem>().Play();
        }
        float recoil = Random.Range(-projectileSpread / 2, projectileSpread / 2);

        spell.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<ProjectileCollision>().damage = Random.Range(minDamage, maxDamage);
    }
}
