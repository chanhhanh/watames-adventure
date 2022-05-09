using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject gameManager;
    [SerializeField]
    float explosionDuration = 1f, damage = 20f;
    [SerializeField]
    bool damageActive = true;

    private void Start()
    {
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(explosionDuration);
        damageActive = false;
    }

    private void FixedUpdate()
    {
        if (transform.childCount == 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && damageActive)
        {
            PlayerStats.Instance.DealDamage(damage);
            damageActive = false;
        }
    }
}
