using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CenterUIInteraction : MonoBehaviour
{
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Перевіряє натискання лівої кнопки миші
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            ClickUI();
        }
    }

    // Натискає кнопку, на яку дивиться гравець
    private void ClickUI()
    {
        if (EventSystem.current == null)
        {
            return;
        }

        // Створює позицію точно в центрі екрана
        PointerEventData pointerData =
            new PointerEventData(EventSystem.current);

        pointerData.position =
            new Vector2(
                Screen.width / 2f,
                Screen.height / 2f
            );

        // Отримує всі UI елементи в центрі екрана
        List<RaycastResult> results =
            new List<RaycastResult>();

        EventSystem.current.RaycastAll(
            pointerData,
            results
        );

        // Перевіряє знайдені UI елементи
        foreach (RaycastResult result in results)
        {
            Button button =
                result.gameObject.GetComponentInParent<Button>();

            if (button != null &&
                button.interactable)
            {
                // Натискає кнопку
                button.onClick.Invoke();

                return;
            }
        }
    }
}