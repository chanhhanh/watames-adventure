using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        DetectPlayerInRange();
    }

    public void DetectPlayerInRange()
    {
        Transform closest = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 pointA = transform.position;
        Vector3 pointB = closest.position;
        float dist = 2.5f;
      
        if ((pointA - pointB).sqrMagnitude < dist * dist)
        {
            StartCoroutine(TickRed());
            StartCoroutine(WaitForExplode());
        }
    }
    IEnumerator TickRed()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(TickRed());
    }
    public GameObject Explosion;
    IEnumerator WaitForExplode()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
