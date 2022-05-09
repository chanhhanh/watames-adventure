using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    public GameObject player;
    public Text playerLevel;
    public Slider healthSlider;
    public Text gem;
    public Slider experienceSlider;
    

    public float health;
    public float maxHealth;

    public float experience;
    public float maxExperience;

    #region Singleton
    public static PlayerStats Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        gem.text = experience.ToString();
        if (!player)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        healthSlider.value = CalculateHealthPercentage();
    }

    public void IncreaseExp(float exp)
    {
        experience += exp;
        //CheckLevelUp();
        //experienceSlider.value = CalculateExpPercentage();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(player);
        }
    }
    float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
   
}
