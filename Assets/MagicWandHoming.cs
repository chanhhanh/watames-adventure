using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWandHoming : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        if (FindClosestEnemy())
        {
            target = FindClosestEnemy().transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Homing();
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

    void Homing()
    {
        if (target)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GetComponent<Rigidbody2D>().rotation = angle;
            GetComponent<Rigidbody2D>().velocity = direction * 7f;
        }
    }
}
