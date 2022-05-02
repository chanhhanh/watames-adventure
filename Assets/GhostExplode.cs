using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostExplode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerInRange();
    }

    public void DetectPlayerInRange()
    {
        GameObject closest = GameObject.FindGameObjectWithTag("Player");
        Vector3 position = transform.position;
        Vector3 diff = closest.transform.position - position;
        if ((diff.x <= 2.5f && diff.x >= -2.5f) && (diff.y <= 2.5f && diff.y >= -2.5f))
        {
            Debug.Log(diff);
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
    IEnumerator WaitForExplode()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Spawner>().enemyCount -= 1;
    }
}
