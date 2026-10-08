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
    public float productionIncrease = 25f;

    [Header("Налаштування покращення")]
    public float startingUpgradeCost = 100f;

    // На скільки відсотків збільшується ціна наступного покращення
    public float upgradeCostIncrease = 20f;

    [Header("Гроші")]
    public MoneyManager moneyManager;

    [Header("Інтерфейс")]
    public TMP_Text levelText;
    public TMP_Text productionText;
    public TMP_Text upgradeCostText;
    public Button upgradeButton;

    private float currentProduction;
    private float currentUpgradeCost;

    // Накопичує частини грошей між секундами
    private float moneyTimer = 0f;

    private void Start()
    {
        currentProduction = startingProduction;
        currentUpgradeCost = startingUpgradeCost;

        // Підключає кнопку покращення
        upgradeButton.onClick.AddListener(UpgradeGenerator);

        UpdateUI();
    }

    private void Update()
    {
        // Додає час до таймера
        moneyTimer += Time.deltaTime;

        // Кожну секунду додаємо гроші
        if (moneyTimer >= 1f)
        {
            moneyTimer -= 1f;

            // Додає точну кількість грошей
            moneyManager.AddMoney(currentProduction);
        }
    }

    // Покращує генератор
    private void UpgradeGenerator()
    {
        // Перевіряє, чи генератор вже досяг максимального рівня
        if (currentLevel >= maxLevel)
        {
            return;
        }

        // Перевіряє, чи достатньо грошей
        if (moneyManager.SpendMoney(currentUpgradeCost))
        {
            // Підвищуємо рівень
            currentLevel++;

            // Збільшує виробництво грошей
            currentProduction *=
                1f + (productionIncrease / 100f);

            // Збільшує ціну наступного покращення
            currentUpgradeCost *=
                1f + (upgradeCostIncrease / 100f);

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
        levelText.text =
            "Рівень " + currentLevel;

        productionText.text =
            "$" +
            currentProduction.ToString("0.00") +
            " / сек";

        // Якщо досягнуто максимального рівня
        if (currentLevel >= maxLevel)
        {
            upgradeCostText.text =
                "МАКСИМАЛЬНИЙ РІВЕНЬ";

            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCostText.text =
                "Покращення: $" +
                Mathf.CeilToInt(currentUpgradeCost);

            upgradeButton.interactable = true;
        }
    }
}