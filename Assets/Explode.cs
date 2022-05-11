using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject Explosion;
    private Transform player;
    private void Awake()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange(2.5f))
        {
            StartCoroutine(TickRed());
            StartCoroutine(WaitForExplode());
        }
    }

    bool PlayerInRange(float dist)
    {
        if (!player) return false;
        if ((transform.position - player.position).sqrMagnitude < dist * dist)
        {
            return true;
        }
        return false;
    }

    IEnumerator TickRed()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(TickRed());
    }
    IEnumerator WaitForExplode()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
