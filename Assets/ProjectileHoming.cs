using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHoming : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    [SerializeField]
    float projectileForce, minSpeedMod, maxSpeedMod, acceleration;
    [SerializeField]
    string affected;
    [SerializeField]
    bool direct = true;
    void Awake()
    {
        if (FindClosestEnemy())
        {
            target = FindClosestEnemy().transform;
        }
    }

    void FixedUpdate()
    {
        Homing();
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(affected);
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
    [SerializeField]
    float turnSpeed;
    void Homing()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        

        switch (direct)
        {
            case true:
                if (target)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    Vector2 direction = (target.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rb.rotation = angle;
                    rb.velocity = direction * projectileForce;
                }
                break;
            case false:
                if (target)
                {
                    rb.velocity = Vector3.RotateTowards(rb.velocity, target.position - transform.position, turnSpeed * (Mathf.PI / 180f) * Time.fixedDeltaTime, 0f);
                    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                    rb.rotation = angle;
                }
                projectileForce += acceleration * Time.fixedDeltaTime;
                projectileForce = Mathf.Clamp(projectileForce, projectileForce * minSpeedMod, projectileForce * maxSpeedMod);
                rb.velocity = rb.velocity.normalized * projectileForce;
                break;
        }
    }
}
