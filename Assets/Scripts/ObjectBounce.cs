using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    [SerializeField]
    GameObject particle;
    [SerializeField]
    string m_tag;
    [SerializeField]
    AudioClip auc;
    [SerializeField]
    float volume = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(m_tag))
        {
            if (auc) AudioSource.PlayClipAtPoint(auc, transform.position, volume);
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
