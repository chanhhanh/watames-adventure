using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header ("Settings Menu Settings")]
    public GameObject m_settingsMenuUI;

    [Header("Pause Menu Settings")]
    public static bool isPaused = false;
    public static bool isReloading = false;
    public GameObject m_pauseMenuUI;

    [Header("Game Over Menu Settings")]
    public GameObject m_gameOverUI;
    public Text m_score;

    [Header("Audio Settings")]
    public AudioSource BGM_Audio;
    public AudioSource SFX_Audio;
    public Slider BGM_Slider;
    public Slider SFX_Slider;
    public static float m_BGMVolume = 1f;
    public static float m_SFXVolume = 1f;
    [Header("Transitions")]
    public Animator m_transition;
    public float m_transitionTime;
    private void Start()
    {
        isPaused = false;
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
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(scene));
    }
    IEnumerator LoadLevel(int scene)
    {
        if(m_transition) m_transition.Play("Transition_Exit");
        yield return new WaitForSecondsRealtime(m_transitionTime);
        SceneManager.LoadSceneAsync(scene);
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
        PlayerStats.m_isDead = false;
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
        m_gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenSettings()
    {
        m_settingsMenuUI.SetActive(true);
    }

    public void CloseSettings()
    {
        m_settingsMenuUI.SetActive(false);
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
