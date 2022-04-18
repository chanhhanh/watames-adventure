using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : MonoBehaviour
{

    public GameObject projectile;

    private float minDamage = 23;
    private float maxDamage = 26;
    public float projectileForce = 3;
    private float cooldown = 2.5f;
    public float spellLevel = 0;
    private Vector2 direction;
    private float angle;
    GameObject spell;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootEnemy()
    {
        if (spellLevel > 5) spellLevel = 5;

        yield return new WaitForSeconds(cooldown);
        SpawnFireball();
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
    public GameObject FindFurthestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject furthest = null;
        float distance = Mathf.NegativeInfinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance > distance)
            {
                furthest = go;
                distance = curDistance;
            }
        }
        return furthest;
    }

    private void SpawnFireball()
    {
        //Fireball position offset
        Vector3 fireballOffset = new Vector3(0.8f, 0.8f, 0f);
        Vector3 fireballOffset2 = new Vector3(-0.8f, -0.8f, 0f);

        spell = Instantiate(projectile, transform.position, Quaternion.identity);
        GameObject enemy = FindClosestEnemy();
        Vector3 myPos = transform.position;
        direction = (enemy.transform.position - myPos).normalized;
        Vector2 direction2 = (enemy.transform.position  - myPos + fireballOffset).normalized;
        Vector2 direction3 = (enemy.transform.position  - myPos + fireballOffset2).normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce * 1.02f;
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);

        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction2 * projectileForce;
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);

        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        angle = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction3 * projectileForce;
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
    }
}
