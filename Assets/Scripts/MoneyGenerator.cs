using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyGenerator : MonoBehaviour
{
    [Header("Налаштування генератора")]
    public int maxLevel = 5;
    public int currentLevel = 1;

    // Початкова кількість грошей за секунду
    public float startingProduction = 10f;

    // На скільки відсотків збільшується виробництво
    public float productionIncrease = 20f;

    [Header("Налаштування покращення")]
    public int startingUpgradeCost = 100;

    // На скільки відсотків збільшується ціна наступного покращення
    public float upgradeCostIncrease = 10f;

    [Header("Гроші")]
    public MoneyManager moneyManager;

    [Header("Інтерфейс")]
    public TMP_Text levelText;
    public TMP_Text productionText;
    public TMP_Text upgradeCostText;
    public Button upgradeButton;

    private float currentProduction;
    private int currentUpgradeCost;

    // Таймер для виробництва грошей
    private float moneyTimer = 0f;

    private void Start()
    {
        currentProduction = startingProduction;
        currentUpgradeCost = startingUpgradeCost;

        upgradeButton.onClick.AddListener(UpgradeGenerator);

        UpdateUI();
    }

    private void Update()
    {
        // Додаємо час до таймера
        moneyTimer += Time.deltaTime;

        // Кожну секунду додаємо гроші
        if (moneyTimer >= 1f)
        {
            moneyTimer -= 1f;

            int moneyToGive = Mathf.RoundToInt(currentProduction);

            moneyManager.AddMoney(moneyToGive);
        }
    }

    // Покращує генератор
    private void UpgradeGenerator()
    {
        // Перевіряємо, чи генератор вже досяг максимального рівня
        if (currentLevel >= maxLevel)
        {
            return;
        }

        // Перевіряємо, чи достатньо грошей
        if (moneyManager.SpendMoney(currentUpgradeCost))
        {
            // Підвищуємо рівень
            currentLevel++;

            // Збільшуємо виробництво грошей
            currentProduction *=
                1f + (productionIncrease / 100f);

            // Збільшуємо ціну наступного покращення
            currentUpgradeCost = Mathf.RoundToInt(
                currentUpgradeCost *
                (1f + upgradeCostIncrease / 100f)
            );

            UpdateUI();

            Debug.Log(
                "Генератор покращено до рівня " +
                currentLevel
            );
        }
        else
        {
            Debug.Log("Недостатньо грошей!");
        }
    }

    // Оновлює інформацію на інтерфейсі
    private void UpdateUI()
    {
        levelText.text = "Рівень " + currentLevel;

        productionText.text =
            "$" + currentProduction.ToString("0.00") + " / сек";

        // Якщо досягнуто максимального рівня
        if (currentLevel >= maxLevel)
        {
            upgradeCostText.text = "МАКСИМАЛЬНИЙ РІВЕНЬ";

            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCostText.text =
                "Покращення: $" + currentUpgradeCost;

            upgradeButton.interactable = true;
        }
    }
}