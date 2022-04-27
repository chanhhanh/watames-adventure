using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{

    public GameObject projectile;

    private float minDamage = 8f;
    private float maxDamage = 11f;
    private float cooldown = 2f;
    public float spellLevel = 0;

    //Cross force
    private float projectileForce = 7f;
    public float torque = 360f;

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
        SpawnCross();
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
    private void SpawnCross()
    {
        spell = Instantiate(projectile, transform.position, Quaternion.identity);

        direction = (FindClosestEnemy().transform.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = direction * (projectileForce * 0.8f);
        spell.GetComponent<Rigidbody2D>().AddTorque(torque);
        spell.GetComponent<CollisionNonDestruct>().damage = Random.Range(minDamage, maxDamage);
        spell.GetComponent<CrossInverse>().projectileForce = projectileForce;
        spell.GetComponent<CrossInverse>().direction = direction;
    }
}
