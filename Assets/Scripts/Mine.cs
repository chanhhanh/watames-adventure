using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float armTime = 1f;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioClip m_audioClip;

    // Start is called before the first frame update
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 0);
        GetComponent<Collider2D>().enabled = false;
    }
    void Start()
    {
        StartCoroutine(StartArming());
    }

    IEnumerator StartArming()
    {
        yield return new WaitForSeconds(armTime);
        animator.Play("Projectile_Mine_Armed");
        AudioSource.PlayClipAtPoint(m_audioClip, transform.position);
        GetComponent<Collider2D>().enabled = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
