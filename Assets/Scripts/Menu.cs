using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    #region Singleton
    public static Menu instance;
    private void Awake()
    {
        instance = this;
    }
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            m_gamepad = true;
        }
    }
    #endregion

    [Header("Settings Menu Settings")]
    public GameObject m_settingsMenuUI;
    public Toggle m_fullScreenToggle;


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
    public static bool fullscreen = true;
    public BGMData m_BGMData;

    [Header("Transitions")]
    public Animator m_transition;
    public float m_transitionTime;

    [Header("Level Selection")]
    [SerializeField] private int m_levelIndex = 1;
    public GameObject m_playMenuUI;
    public Text m_levelLabel;
    public Text m_difficultyLabel;
    public static bool m_oneHealthMode = false;
    public List<LevelData> levels;

    public static bool m_gamepad = true;
    public GameObject m_leftThumbstick;
    public GameObject m_rightThumbstick;
    private void Start()
    {
        isPaused = false;
        isReloading = false;
        BGM_Slider.value = m_BGMVolume;
        BGM_Audio.volume = m_BGMVolume;
        SFX_Slider.value = m_SFXVolume;
        SFX_Audio.volume = m_SFXVolume;
        Screen.fullScreen = fullscreen;
        m_fullScreenToggle.isOn = fullscreen;
        if (m_levelLabel) m_levelLabel.text = levels[m_levelIndex-1].level.name;
        PlayBGM();
        if (m_gamepad)
        {
            m_leftThumbstick.SetActive(true);
            m_rightThumbstick.SetActive(true);
        }
    }
    void OnApplicationQuit()
    {
        isReloading = true;
    }
    public void PlayBGM()
    {
        BGM_Audio.loop = true;
        BGM_Audio.clip = m_BGMData.bgm.main;
        BGM_Audio.Play();
    }
    public IEnumerator PlayBossBGM()
    {
        BGM_Audio.loop = false;
        BGM_Audio.clip = m_BGMData.bgm.transition;
        BGM_Audio.Play();
        yield return new WaitForSeconds(BGM_Audio.clip.length);
        BGM_Audio.clip = m_BGMData.bgm.boss;
        BGM_Audio.Play();
        BGM_Audio.loop = true;
    }

    public void SwitchStage(int newIndex)
    {
        m_levelIndex = m_levelIndex + newIndex;
        if (m_levelIndex > levels.Count) m_levelIndex = 1;
        if (m_levelIndex < 1) m_levelIndex = levels.Count;
        m_levelLabel.text = levels[m_levelIndex - 1].level.name;
    }
    public void ChangeDifficulty()
    {
        if (m_oneHealthMode)
        {
            m_difficultyLabel.text = "Normal Mode";
        }
        else m_difficultyLabel.text = "One Health";
        m_oneHealthMode = !m_oneHealthMode;
    }
    public void LoadScene()
    {
        isReloading = true;
        isPaused = false;
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(m_levelIndex));
    }
    public void ReturnToMenu()
    {
        isReloading = true;
        isPaused = false;
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(0));
    }
    IEnumerator LoadLevel(int scene)
    {
        if (m_transition) m_transition.Play("Transition_Exit");
        yield return new WaitForSecondsRealtime(m_transitionTime);
        SceneManager.LoadSceneAsync(scene);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && m_pauseMenuUI) Resume();
            else if (!isPaused && m_pauseMenuUI) Pause();
            if (m_playMenuUI) ClosePlayMenu();
            if (m_settingsMenuUI) CloseSettings();
        }
        if (PlayerStats.m_isDead)
        {
            StartCoroutine(GameOver());
        }
    }

    public void AdjustBGMAudio(Slider audio)
    {
        m_BGMVolume = audio.value;
        BGM_Audio.volume = m_BGMVolume;
    }
    public void AdjustSFXAudio(Slider audio)
    {
        m_SFXVolume = audio.value;
        SFX_Audio.volume = m_SFXVolume;
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
        m_fullScreenToggle.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
    }

    public void CloseSettings()
    {
        m_settingsMenuUI.SetActive(false);
        m_fullScreenToggle.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
    }

    public void OpenPlayMenu()
    {
        m_playMenuUI.SetActive(true);
    }

    public void ClosePlayMenu()
    {
        m_playMenuUI.SetActive(false);
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

    public void SetFullscreen()
    {
        if (!fullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            fullscreen = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            fullscreen = false;
        }
    }
}

