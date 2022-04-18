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
    public Slider experienceSlider;
    

    public float health;
    public float maxHealth;

    public float experience;
    public float maxExperience;
    private int level = 1;

    //Upgrades
    const string MagicWand = "MagicWand";
    const string Whip = "Whip";
    string[] upgrades = { "MagicWand", "Whip" };

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
        CheckLevelUp();
        experienceSlider.value = CalculateExpPercentage();
    }

    private void CheckLevelUp()
    {
        if (experience >= maxExperience)
        {
            level += 1;
            playerLevel.text = "LVL " + level;
            maxExperience += maxExperience*0.5f;
            experience = 0;
            Upgrade(MagicWand);
            Upgrade(Whip);
        }
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
    float CalculateExpPercentage()
    {
        return experience / maxExperience;
    }
    private void Upgrade(string toUpgrade)
    {
        switch (toUpgrade)
        {
            case MagicWand:
                player.GetComponent<MagicWand>().spellLevel += 1;
                break;
            case Whip:
                player.GetComponent<Whip>().spellLevel += 1;
                break;
        }
    }
}
