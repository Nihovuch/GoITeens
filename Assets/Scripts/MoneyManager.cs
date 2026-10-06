using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money = 0;

    [Header("Налаштування інтерфейсу")]
    public TMP_Text moneyText;

    private void Start()
    {
        UpdateMoneyUI();
    }

    // Додає гроші гравцю
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    // Перевіряє, чи достатньо грошей, і забирає їх
    public bool SpendMoney(int amount)
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
            moneyText.text = "$" + money;
        }
    }
}