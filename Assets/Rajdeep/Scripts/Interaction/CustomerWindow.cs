using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWindow : MonoBehaviour, IInteractable
{
    private OrderData currentOrder;

    private List<IngredientType> remainingIngredients =
        new List<IngredientType>();

    private ScoreManager scoreManager;
    private OrderManager orderManager;

    [SerializeField] private float newOrderDelay = 5f;

    [Header("Score Popup")]
    [SerializeField] private OrderScorePopup scorePopup;
    [SerializeField] private Transform scorePopupPosition;

    public void SetOrder(OrderData order)
    {
        currentOrder = order;

        remainingIngredients =
            new List<IngredientType>(
                order.requiredIngredients
            );

        Debug.Log(
            gameObject.name +
            " received a new order with " +
            remainingIngredients.Count +
            " ingredients."
        );

        Debug.Log(
            "Order: " +
            string.Join(
                ", ",
                remainingIngredients
            )
        );

        RefreshOrderUI();
    }

    public OrderData GetCurrentOrder()
    {
        return currentOrder;
    }

    public List<IngredientType> GetRemainingIngredients()
    {
        return remainingIngredients;
    }

    public void Interact()
    {
        if (currentOrder == null)
        {
            Debug.Log(
                "This customer window has no order."
            );

            return;
        }

        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            Debug.LogWarning(
                "PlayerItemHolder not found."
            );

            return;
        }

        scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogWarning(
                "ScoreManager not found."
            );

            return;
        }

        orderManager =
            FindFirstObjectByType<OrderManager>();

        if (orderManager == null)
        {
            Debug.LogWarning(
                "OrderManager not found."
            );

            return;
        }

        if (!itemHolder.IsHoldingItem)
        {
            Debug.Log(
                "Player is not holding an ingredient."
            );

            return;
        }

        GameObject heldObject =
            itemHolder.GetHeldItem();

        Ingredient ingredient =
            heldObject.GetComponent<Ingredient>();

        if (ingredient == null)
        {
            Debug.LogWarning(
                "Held object is not an Ingredient."
            );

            return;
        }

        // Vegetable must be chopped
        if (ingredient.Type ==
            IngredientType.Vegetable &&
            !ingredient.IsPrepared)
        {
            Debug.Log(
                "Vegetable must be chopped before serving."
            );

            return;
        }

        // Meat must be cooked
        if (ingredient.Type ==
            IngredientType.Meat &&
            !ingredient.IsCooked)
        {
            Debug.Log(
                "Meat must be cooked before serving."
            );

            return;
        }

        // Cheese does not require preparation

        IngredientType deliveredType =
            ingredient.Type;

        int ingredientIndex =
            remainingIngredients.IndexOf(
                deliveredType
            );

        // Ingredient is not required
        if (ingredientIndex == -1)
        {
            Debug.Log(
                "Wrong ingredient! " +
                deliveredType +
                " is not required by this order."
            );

            return;
        }

        // Remove one matching ingredient
        remainingIngredients.RemoveAt(
            ingredientIndex
        );

        // Remove ingredient from player's hand
        itemHolder.ClearHeldItem();

        Destroy(heldObject);

        Debug.Log(
            "Correct ingredient delivered: " +
            deliveredType
        );

        // Check if entire order is completed
        if (remainingIngredients.Count == 0)
        {
            currentOrder.IsCompleted = true;

            // Calculate how long the order was open
            float elapsedTime =
                Time.time -
                currentOrder.activationTime;

            // Calculate final order score
            int orderScore =
                scoreManager.CalculateOrderScore(
                    currentOrder,
                    elapsedTime
                );

            // Add final order score
            scoreManager.AddOrderScore(
                orderScore
            );

            // Show score popup
            ShowScorePopup(orderScore);

            Debug.Log(
                gameObject.name +
                " order completed!"
            );

            Debug.Log(
                "Order took " +
                elapsedTime.ToString("F2") +
                " seconds."
            );

            Debug.Log(
                "Final Order Score: " +
                orderScore
            );

            // Wait 5 seconds before new order
            StartCoroutine(
                GenerateNextOrderAfterDelay()
            );
        }
        else
        {
            Debug.Log(
                "Remaining ingredients: " +
                string.Join(
                    ", ",
                    remainingIngredients
                )
            );

            RefreshOrderUI();
        }
    }

    private void ShowScorePopup(int score)
    {
        if (scorePopup == null)
        {
            Debug.LogWarning(
                "Score Popup is not assigned for " +
                gameObject.name
            );

            return;
        }

        if (scorePopupPosition == null)
        {
            Debug.LogWarning(
                "Score Popup Position is not assigned for " +
                gameObject.name
            );

            return;
        }

        scorePopup.transform.position =
            scorePopupPosition.position;

        scorePopup.ShowScore(score);
    }

    private IEnumerator GenerateNextOrderAfterDelay()
    {
        RefreshOrderUI();

        Debug.Log(
            "New order will appear in " +
            newOrderDelay +
            " seconds."
        );

        yield return new WaitForSeconds(
            newOrderDelay
        );

        orderManager.GenerateNewOrder(
            this
        );
    }

    private void RefreshOrderUI()
    {
        OrderUI orderUI =
            FindFirstObjectByType<OrderUI>();

        if (orderUI != null)
        {
            orderUI.RefreshOrders();
        }
    }
}