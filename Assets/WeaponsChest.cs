using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsChest : MonoBehaviour
{
    public GameObject player;
    public GameObject currentWeapon;
    public GameObject[] availableWeapons;
    bool taken = false;
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        availableWeapons = new GameObject[player.transform.childCount-1];
        for (int i = 0; i < player.transform.childCount; ++i)
        {
            GameObject weapon = player.transform.GetChild(i).gameObject;
            if (weapon.activeSelf == true)
            {
                currentWeapon = weapon;
            }
            else if (weapon.activeSelf == false && weapon.name != "handgun")
            {
                availableWeapons[index] = weapon;
                index++;
                Debug.Log(availableWeapons);
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name == "Player" && !taken)
        {
            taken = true;
            int rand = Random.Range(0, availableWeapons.Length);
            availableWeapons[rand].SetActive(true);
            currentWeapon.SetActive(false);
            Destroy(gameObject);
        }
    }
}
