using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsChest : MonoBehaviour
{
    [SerializeField]
    private GameObject player, currentWeapon;

    [System.Serializable]
    public class Weapons
    {
        public string tag;
        public GameObject prefab;
    }

    [SerializeField]
    float timeToStart = 5;
    float interval = 5;
    public List<Weapons> weapons;

    // Start is called before the first frame update
    private int rand;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentWeapon = player.transform.GetChild(0).gameObject;
        rand = Random.Range(0, weapons.Count);
        Debug.Log(weapons[rand].prefab);
        Debug.Log(ChestSpawner.instance.currentWeapon);

        while (weapons[rand].prefab == ChestSpawner.instance.currentWeapon)
        {
            rand = Random.Range(0, weapons.Count);
        }
    }
    private void FixedUpdate()
    {
        if (Time.time >= timeToStart)
        {
            timeToStart = interval + timeToStart;
            GetComponent<Animator>().Play("Box_Animation");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            foreach (Transform child in player.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject weapon = Instantiate(weapons[rand].prefab, weapons[rand].prefab.transform.position, Quaternion.identity);
        Transform local = weapons[rand].prefab.transform;
        Debug.Log(weapons[rand].prefab.transform.position);
        Debug.Log(weapons[rand].prefab.transform.rotation);
        Debug.Log(local.lossyScale);
        weapon.transform.SetParent(player.transform);
        weapon.transform.localPosition = local.position;
       
        weapon.transform.localRotation = local.rotation;
        weapon.transform.localScale = local.lossyScale;
        ChestSpawner.instance.chestSpawned = false;
        ChestSpawner.instance.currentWeapon = weapons[rand].prefab;
    }
}
