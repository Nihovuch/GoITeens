using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Налаштування гри")]
    public int targetMoney = 500;
    public float gameTime = 300f;

    [Header("Посилання")]
    public MoneyManager moneyManager;

    [Header("Інтерфейс")]
    public TMP_Text targetText;
    public TMP_Text timerText;

    [Header("Екрани")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Кнопки")]
    public Button winRestartButton;
    public Button loseRestartButton;

    private float currentTime;
    private bool gameFinished = false;

    private void Start()
    {
        currentTime = gameTime;

        // Ховаємо екрани перемоги та поразки
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        // Показуємо цільову кількість грошей
        targetText.text = "Ціль: $" + targetMoney;

        // Оновлюємо таймер
        UpdateTimerUI();

        // Підключаємо кнопки перезапуску
        winRestartButton.onClick.AddListener(RestartGame);
        loseRestartButton.onClick.AddListener(RestartGame);
    }

    private void Update()
    {
        // Якщо гра вже закінчилась, нічого не робимо
        if (gameFinished)
        {
            return;
        }

        // Перевіряємо, чи гравець досягнув цілі
        if (moneyManager.money >= targetMoney)
        {
            WinGame();
            return;
        }

        // Віднімаємо час
        currentTime -= Time.deltaTime;

        // Оновлюємо таймер
        UpdateTimerUI();

        // Перевіряємо, чи час закінчився
        if (currentTime <= 0f)
        {
            currentTime = 0f;

            UpdateTimerUI();

            LoseGame();
        }
    }

    // Оновлює текст таймера
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text =
            "Час: " +
            minutes.ToString("00") +
            ":" +
            seconds.ToString("00");
    }

    // Перемога
    private void WinGame()
    {
        gameFinished = true;

        // Показуємо екран перемоги
        winPanel.SetActive(true);

        Debug.Log("Ви досягли цілі!");
    }

    // Поразка
    private void LoseGame()
    {
        gameFinished = true;

        // Показуємо екран поразки
        losePanel.SetActive(true);

        Debug.Log("Час закінчився! Ви програли!");
    }

    // Перезапускає поточну сцену
    private void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}