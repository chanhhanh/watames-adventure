using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject gameManager;
    public float damage = 20f;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) Destroy(gameObject);
    }

    bool damageDealt = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !damageDealt)
        {
            gameManager.GetComponent<PlayerStats>().DealDamage(damage);
            damageDealt = true;
        }
    }
}
