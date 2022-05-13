using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject projectile;

    [SerializeField]
    private float minDamage = 49, maxDamage = 51, cooldown = 1f;
    public float projectileForce = 1f;
    private bool offCooldown = true;
    GameObject spell;

    //Audio
    public AudioClip bulletSound;
    [SerializeField]
    GameObject source;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && offCooldown && !Menu.isPaused)
        {
            SpawnBullet();
            SwingWeapon();
            if (bulletSound)
            {
                AudioSource.PlayClipAtPoint(bulletSound, transform.position);
            }
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(0.2f, 0.04f));
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
    void SwingWeapon()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 direction = (mousePos - transform.position).normalized;
        //if (direction.x > 0f)
        //{
        //    animator.Play("Weapon_Swing_Right");
        //}
        //else if (direction.x < 0f)
        //{
        //    animator.Play("Weapon_Swing_Left");
        //}
        animator.Play("Weapon_Swing_Right");
    }

    private void SpawnBullet()
    {
        spell = Instantiate(projectile, source.transform.position, transform.rotation);
        spell.GetComponent<Rigidbody2D>().AddForce(transform.parent.right * projectileForce, ForceMode2D.Impulse);
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
    }
}
