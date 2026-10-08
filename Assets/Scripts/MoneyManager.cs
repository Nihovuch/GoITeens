using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [Header("Гроші")]
    public float money = 0f;

    [Header("Налаштування інтерфейсу")]
    public TMP_Text moneyText;

    private void Start()
    {
        UpdateMoneyUI();
    }

    // Додає гроші гравцю
    public void AddMoney(float amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    // Перевіряє, чи достатньо грошей, і забирає їх
    public bool SpendMoney(float amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();

            return true;
        }

        return false;
    }

    // Оновлює текст із кількістю грошей
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + Mathf.FloorToInt(money);
        }
    }
}