using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        for (int i=0; i< 5; ++i)
        {
            ShootProjectile();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(StartShooting());
    }

    private void ShootProjectile()
    {
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        spell.GetComponent<EnemyProjectileCollision>().damage = Random.Range(23, 25);
    }
}
