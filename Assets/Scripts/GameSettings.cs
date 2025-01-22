using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettings : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Settings UI Elements")]
    [SerializeField] private Slider shuffleIntervalSlider;
    [SerializeField] private Slider penaltyPercentageSlider;
    [SerializeField] private Slider gameTimeSlider;

    [Header("Display Text")]
    [SerializeField] private TextMeshProUGUI shuffleIntervalText;
    [SerializeField] private TextMeshProUGUI penaltyPercentageText;
    [SerializeField] private TextMeshProUGUI gameTimeText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Initialize UI elements with current values
        InitializeUI();

        // Add listeners
        shuffleIntervalSlider.onValueChanged.AddListener(OnShuffleIntervalChanged);
        penaltyPercentageSlider.onValueChanged.AddListener(OnPenaltyPercentageChanged);
        gameTimeSlider.onValueChanged.AddListener(OnGameTimeChanged);


    }

    private void InitializeUI()
    {
        // Set initial slider values from GameManager
        shuffleIntervalSlider.value = gameManager.ShuffleInterval;
        penaltyPercentageSlider.value = gameManager.PenaltyPercentage;
        gameTimeSlider.value = gameManager.GameTime;

        // Update texts
        UpdateAllTexts();
    }

    private void UpdateAllTexts()
    {
        shuffleIntervalText.text = $"Shuffle Interval: {shuffleIntervalSlider.value:F1}s";
        penaltyPercentageText.text = $"Penalty: {penaltyPercentageSlider.value}%";
        gameTimeText.text = $"Game Time: {gameTimeSlider.value}s";
    }

    public void TogglePanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void OnShuffleIntervalChanged(float value)
    {
        shuffleIntervalText.text = $"Shuffle Interval: {value:F1}s";
        gameManager.ShuffleInterval = value;
    }

    private void OnPenaltyPercentageChanged(float value)
    {
        penaltyPercentageText.text = $"Penalty: {value}%";
        gameManager.PenaltyPercentage = value;
    }

    private void OnGameTimeChanged(float value)
    {
        gameTimeText.text = $"Game Time: {value}s";
        gameManager.GameTime = value;
    }


}