using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsChest : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [System.Serializable]
    public class Weapons
    {
        public string tag;
        public GameObject prefab;
    }

    [SerializeField]
    float timeToStart = 3;
    public List<Weapons> weapons;
    public AudioClip m_audioClip;

    // Start is called before the first frame update
    private int rand;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rand = Random.Range(0, weapons.Count);

        while (weapons[rand].tag == ChestSpawner.instance.m_weapon.m_tag)
        {
            rand = Random.Range(0, weapons.Count);
        }
        StartCoroutine(BoxAnimation());
    }

    IEnumerator BoxAnimation()
    {
        yield return new WaitForSeconds(timeToStart);
        GetComponent<Animator>().Play("Box_Animation");
        StartCoroutine(BoxAnimation());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Destroy(ChestSpawner.instance.m_weapon.m_currentWeapon);
            if (m_audioClip) AudioSource.PlayClipAtPoint(m_audioClip, transform.position, Menu.m_SFXVolume);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!Menu.isReloading)
        {
            GameObject weapon = Instantiate(weapons[rand].prefab, weapons[rand].prefab.transform.position, Quaternion.identity);
            Transform local = weapons[rand].prefab.transform;

            weapon.transform.SetParent(player.transform);
            weapon.transform.localPosition = local.position;

            weapon.transform.localRotation = local.rotation;
            weapon.transform.localScale = local.lossyScale;
            ChestSpawner.instance.chestSpawned = false;
            ChestSpawner.instance.m_weapon.m_currentWeapon = weapon;
            ChestSpawner.instance.m_weapon.m_tag = weapons[rand].tag;
            GameManager.Instance.m_box.boxCount += 1;
            GameManager.Instance.UpdateBoxCount();
        }
    }
}
