using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [System.Serializable]
    public struct Player
    {
        public GameObject player;
        public float health;
        public float maxHealth;
        public Slider healthSlider;
        public Image cooldownIndicator;
    }

    [System.Serializable]
    public struct Box
    {
        public Text box;
        public float boxCount;
    }

    [System.Serializable]
    public struct Boss
    {
        public Text m_bossName;

        public Image m_bar;

        public Image m_damageBar;

        public float m_damageBarDepleteRate;

        public GameObject m_boss;
    }

    public Camera m_uiCamera;
    public Transform m_crosshair;
    public GameObject m_bossBar;
    public AudioClip m_BossSFX;
    public AudioSource m_audioSource;
    public Player m_player;
    public Box m_box;
    public Boss m_boss;
    public static bool m_isDead = false;
    public int BossStartsAtBox = 11;
    #region Singleton
    public static PlayerStats Instance;

    private void Awake()
    {
        Instance = this;
        if (Menu.m_oneHealthMode) m_player.maxHealth = (float)0.01;
        m_player.health = m_player.maxHealth;
        m_player.healthSlider.value = 1;
        m_player.cooldownIndicator.fillAmount = 0;
        HideBossBar();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCrosshair();
    }
    public void InitBoss(string name, GameObject go)
    {
        m_boss.m_bossName.text = name;
        m_boss.m_boss = go;
        m_boss.m_bar.fillAmount = 1;
        m_bossBar.SetActive(true);
        m_audioSource.clip = m_BossSFX;
        m_audioSource.Play();
        StartCoroutine(Menu.instance.PlayBossBGM());
    }
    public void HideBossBar()
    {
        m_bossBar.SetActive(false);
        Menu.instance.PlayBGM();
    }
    public void MoveCrosshair()
    {
        Vector2 mousePos = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
        m_crosshair.position = mousePos;
    }
    public IEnumerator UpdateBossHealth(float m_health, float m_currentMaxHealth)
    {
        m_boss.m_bar.fillAmount = m_health / m_currentMaxHealth;
        while (m_boss.m_bar.fillAmount != m_boss.m_damageBar.fillAmount)
        {
            m_boss.m_damageBar.fillAmount = Mathf.Clamp(m_boss.m_damageBar.fillAmount - m_boss.m_damageBarDepleteRate * Time.deltaTime, m_boss.m_bar.fillAmount, 1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void UpdateBoxCount()
    {
        m_box.box.text = m_box.boxCount.ToString();
        m_player.cooldownIndicator.fillAmount = 0;
        if (m_box.boxCount == BossStartsAtBox)
        {
            BossStartsAtBox += 10;
            EnemySpawner.instance.SpawnBoss();
        }
    }
    public void DealDamage(float damage)
    {
        m_player.health -= damage;
        CheckDeath();
        m_player.healthSlider.value = CalculateHealthPercentage();
    }
    private void CheckDeath()
    {
        if (m_player.health <= 0)
        {
            m_isDead = true;
            Destroy(m_player.player);
            m_player.healthSlider.gameObject.SetActive(false);
            m_player.cooldownIndicator.fillAmount = 0;
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles)
                Destroy(projectile);
        }
    }
    float CalculateHealthPercentage()
    {
        return m_player.health / m_player.maxHealth;
    }
    public IEnumerator VisualizeCooldown(float cooldown)
    {
        float elapsed = 0f;
        while (elapsed < cooldown)
        {
            elapsed += Time.deltaTime;

            m_player.cooldownIndicator.fillAmount = elapsed / cooldown;
            yield return null;
        }
        m_player.cooldownIndicator.fillAmount = 0f;
    }
}
