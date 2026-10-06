using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyableWall : MonoBehaviour
{
    [Header("Налаштування стіни")]
    public int price = 500;

    [Header("Посилання")]
    public GameObject wall;
    public Canvas purchaseCanvas;
    public Button purchaseButton;
    public TMP_Text priceText;

    [Header("Гроші")]
    public MoneyManager moneyManager;

    [Header("Генератори кімнати")]
    public MoneyGenerator generator1;
    public MoneyGenerator generator2;

    private void Start()
    {
        // Показуємо ціну кімнати
        priceText.text = "$" + price;

        // Підключаємо метод покупки до кнопки
        purchaseButton.onClick.AddListener(PurchaseRoom);

        // На початку обидва генератори вимкнені
        if (generator1 != null)
        {
            generator1.enabled = false;
        }

        if (generator2 != null)
        {
            generator2.enabled = false;
        }
    }

    // Купівля кімнати
    private void PurchaseRoom()
    {
        // Перевіряємо, чи достатньо грошей
        if (moneyManager.SpendMoney(price))
        {
            Debug.Log("Кімнату придбано!");

            // Ховаємо стіну разом з її колайдером
            if (wall != null)
            {
                wall.SetActive(false);
            }

            // Вмикаємо перший генератор
            if (generator1 != null)
            {
                generator1.enabled = true;
            }

            // Вмикаємо другий генератор
            if (generator2 != null)
            {
                generator2.enabled = true;
            }

            // Ховаємо меню покупки
            if (purchaseCanvas != null)
            {
                purchaseCanvas.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Недостатньо грошей!");
        }
    }
}