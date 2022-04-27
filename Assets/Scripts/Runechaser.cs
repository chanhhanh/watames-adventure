using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runechaser : MonoBehaviour
{

    public GameObject projectile;

    private float minDamage = 8f;
    private float maxDamage = 11f;
    private float cooldown = 3f;
    public float spellLevel = 0;

    private float projectileForce = 7f;

    //Cross transform
    private Vector2 direction;
    private float angle;
    GameObject spell;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootEnemy());
    }

    IEnumerator ShootEnemy()
    {
        yield return new WaitForSeconds(cooldown);
        SpawnRunetracer();
        StartCoroutine(ShootEnemy());
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void SpawnRunetracer()
    {
        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        direction = (FindClosestEnemy().transform.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
    }
}
