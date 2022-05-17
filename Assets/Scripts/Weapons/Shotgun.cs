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
    public float cooldown = 2f;
    GameObject spell;

    //Audio
    public AudioClip bulletSound;
    [SerializeField]
    GameObject source;

    private float inputHorizontal;
    private float inputVertical;
    private void FixedUpdate()
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
        offCooldown = false;
        StartCoroutine(GameManager.Instance.VisualizeCooldown(cooldown));
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }
 
    private void SpawnBullet()
    {
        if (bulletSound)
        {
            AudioSource.PlayClipAtPoint(bulletSound, transform.position, Menu.m_SFXVolume);
        }
        StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.1f, 0.02f));
       
        Vector2 mousePos = GameManager.Instance.m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        if (Menu.m_gamepad)
        {
            direction = new Vector2(inputHorizontal, inputVertical);
        }

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
