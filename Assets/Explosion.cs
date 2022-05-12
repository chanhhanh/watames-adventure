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
    [SerializeField]
    float screenShakeDuration, screenShakeMagnitude;
    [SerializeField]
    string affected;
    [SerializeField]
    AudioClip aud;

    private void Start()
    {
        StartCoroutine(DealDamage());
        if (screenShakeDuration != 0f && screenShakeMagnitude != 0f)
        {
            StartCoroutine(PlayerCamera.Instance.ShakeOnce(screenShakeMagnitude, screenShakeDuration));
        }
        if(aud)
        {
            AudioSource.PlayClipAtPoint(aud, transform.position);
        }
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
        if (collision.CompareTag(affected) && damageActive)
        {
            switch (affected)
            {
                case "Player":
                    PlayerStats.Instance.DealDamage(damage);
                    damageActive = false;
                    break;
                case "Enemy":
                    Vector2 direction = ((Vector2)collision.transform.position - (Vector2)transform.position);
                    collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage, direction);
                    break;
            }
        }
    }
}
