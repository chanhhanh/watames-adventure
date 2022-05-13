using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject settingsPanel;
    [Header("Pause Menu Settings")]
    public static bool isPaused = false;
    public static bool isReloading = false;
    public GameObject m_pauseMenuUI;

    [Header("Game Over Menu Settings")]
    public GameObject m_gameOverUI;
    public Text m_score;

    private void Start()
    {
        isReloading = false;
    }
    void OnApplicationQuit()
    {
        isReloading = true;
    }
    public void LoadScene(int scene)
    {
        isReloading = true;
        isPaused = false;
        PlayerStats.m_isDead = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_pauseMenuUI)
        {
            if (isPaused) Resume();
            else Pause();
        }
        if(PlayerStats.m_isDead)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        m_gameOverUI.SetActive(true);
        m_score.text = PlayerStats.Instance.m_box.box.text;
        Cursor.visible = true;
    }

    public void Retry()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        isReloading = true;
        PlayerStats.m_isDead = false;
        m_gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Cursor.visible = false;
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
    public void Pause()
    {
        Cursor.visible = true;
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
