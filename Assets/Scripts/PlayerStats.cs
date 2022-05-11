using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    public GameObject player;
    public Slider healthSlider;
    public Text box;
    

    public float health;
    public float maxHealth;

    public float boxCount;

    public Image cooldownIndicator;
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
        cooldownIndicator.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
    }

    public void UpdateBoxCount()
    {
        box.text = boxCount.ToString();
        cooldownIndicator.fillAmount = 0;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        healthSlider.value = CalculateHealthPercentage();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(player);
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectiles");
            foreach (GameObject projectile in projectiles)
                Destroy(projectile);
        }
    }
    float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
   
    public IEnumerator VisualizeCooldown(float cooldown)
    {
        float elapsed = 0f;
        while (elapsed < cooldown)
        {
            elapsed += Time.deltaTime;

            cooldownIndicator.fillAmount = elapsed / cooldown;
            yield return null;
        }
        cooldownIndicator.fillAmount = 0f;
    }
}
