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
            return;
        }

        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            return;
        }

        scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            return;
        }

        orderManager =
            FindFirstObjectByType<OrderManager>();

        if (orderManager == null)
        {
            return;
        }

        if (!itemHolder.IsHoldingItem)
        {
            return;
        }

        GameObject heldObject =
            itemHolder.GetHeldItem();

        Ingredient ingredient =
            heldObject.GetComponent<Ingredient>();

        if (ingredient == null)
        {
            return;
        }

        // Vegetable must be chopped
        if (ingredient.Type ==
            IngredientType.Vegetable &&
            !ingredient.IsPrepared)
        {
            return;
        }

        // Meat must be cooked
        if (ingredient.Type ==
            IngredientType.Meat &&
            !ingredient.IsCooked)
        {
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
            return;
        }

        // Remove one matching ingredient
        remainingIngredients.RemoveAt(
            ingredientIndex
        );

        // Remove ingredient from player's hand
        itemHolder.ClearHeldItem();

        Destroy(heldObject);

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

            // Wait 5 seconds before new order
            StartCoroutine(
                GenerateNextOrderAfterDelay()
            );
        }
        else
        {
            RefreshOrderUI();
        }
    }

    private void ShowScorePopup(int score)
    {
        if (scorePopup == null)
        {
            return;
        }

        if (scorePopupPosition == null)
        {
            return;
        }

        scorePopup.transform.position =
            scorePopupPosition.position;

        scorePopup.ShowScore(score);
    }

    private IEnumerator GenerateNextOrderAfterDelay()
    {
        RefreshOrderUI();

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