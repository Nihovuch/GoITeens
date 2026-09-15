using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyableWall : MonoBehaviour
{
    public int price = 500;

    public GameObject wall;
    public Canvas purchaseCanvas;
    public Button purchaseButton;
    public TMP_Text priceText;

    public MoneyManager moneyManager;

    private Renderer wallRenderer;
    private Collider wallCollider;

    private void Start()
    {
        priceText.text = "$" + price;

        purchaseButton.onClick.AddListener(PurchaseWall);

        wallRenderer = wall.GetComponent<Renderer>();
        wallCollider = wall.GetComponent<Collider>();

        if (wallCollider != null)
        {
            wallCollider.enabled = false;
        }
    }

    private void PurchaseWall()
    {
        if (moneyManager.SpendMoney(price))
        {
            MakeWallPurchased();
            purchaseCanvas.gameObject.SetActive(false);

            Debug.Log("Wall purchased!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    private void MakeWallPurchased()
    {
        if (wallRenderer != null)
        {
            Color wallColor = wallRenderer.material.color;
            wallColor.a = 1f;
            wallRenderer.material.color = wallColor;
        }

        if (wallCollider != null)
        {
            wallCollider.enabled = true;
        }
    }
}