using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private Slider dashCooldownSlider;
    [SerializeField] private Slider immuneCooldownSlider;
    [SerializeField] private TMP_Text experienceText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestTime;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject levelUpPanel;

    public LevelUpButton[] LevelUpButtons;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        UpdateBestScore(GameManager.Instance.bestScore);
    }

    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = PlayerController.Instance.playerMaxHealth;
        playerHealthSlider.value = PlayerController.Instance.playerHealth;
        healthText.text = playerHealthSlider.value + "/" + playerHealthSlider.maxValue;
    }

    public void UpdateExperienceSlider()
    {
        playerExperienceSlider.maxValue = PlayerController.Instance.playerLevels[PlayerController.Instance.currentLevel - 1];
        playerExperienceSlider.value = PlayerController.Instance.experience;
        experienceText.text = "Level " + PlayerController.Instance.currentLevel + ": " + playerExperienceSlider.value + "/" + playerExperienceSlider.maxValue;
    }

    public void UpdateImmuneCooldownSlider(float currentCooldown, float maxCooldown)
    {
        immuneCooldownSlider.maxValue = maxCooldown;
        immuneCooldownSlider.value = currentCooldown;
        // Nilai slider akan naik dari 0 → maxCooldown saat cooldown berakhir
    }

    public void UpdateDashCooldownSlider(float currentCooldown, float maxCooldown)
    {
        dashCooldownSlider.maxValue = maxCooldown;
        dashCooldownSlider.value = maxCooldown - currentCooldown;
        // Nilai slider akan naik dari 0 → maxCooldown saat cooldown berakhir
    }

    public void UpdateTimer(float time)
    {
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);

        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public void LevelUpPanelOpen()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LevelUpPanelClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateBestScore(float bestScore)
    {
        float min = Mathf.FloorToInt(bestScore / 60f);
        float sec = Mathf.FloorToInt(bestScore % 60f);

        bestTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }

}
