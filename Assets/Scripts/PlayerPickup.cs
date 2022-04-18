using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float PickUpSpeed = 4f;
    public float ExpValue = 10f;
    private GameObject gameManager;

    private void Start()
    {
    gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            
            StartCoroutine(PickUp());
        }
    }
    IEnumerator PickUp()
    {
        yield return new WaitForSeconds(0f);
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, PickUpSpeed * Time.deltaTime);
        if (transform.position == player.transform.position)
        {
           gameManager.GetComponent<PlayerStats>().IncreaseExp(ExpValue);
           Destroy(gameObject);
        }
        StartCoroutine(PickUp());
    }
}
