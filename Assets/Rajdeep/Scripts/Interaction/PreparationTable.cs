using System.Collections;
using UnityEngine;

public class PreparationTable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform prepPoint;
    [SerializeField] private float preparationTime = 2f;

    private Ingredient currentIngredient;
    private bool isPreparing;

    private float remainingPreparationTime;

    public bool IsPreparing => isPreparing;

    public float RemainingPreparationTime =>
        remainingPreparationTime;

    public void Interact()
    {
        if (isPreparing)
        {
            return;
        }

        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            return;
        }

        // PLAYER IS HOLDING AN ITEM
        if (itemHolder.IsHoldingItem)
        {
            // Table can only hold one ingredient
            if (currentIngredient != null)
            {
                return;
            }

            GameObject ingredientObject =
                itemHolder.GetHeldItem();

            Ingredient ingredient =
                ingredientObject.GetComponent<Ingredient>();

            if (ingredient == null)
            {
                return;
            }

            // Only vegetables can be prepared
            if (ingredient.Type !=
                IngredientType.Vegetable)
            {
                return;
            }

            if (ingredient.IsPrepared)
            {
                return;
            }

            ingredientObject.transform.SetParent(
                prepPoint
            );

            ingredientObject.transform.localPosition =
                Vector3.zero;

            ingredientObject.transform.localRotation =
                Quaternion.identity;

            Collider ingredientCollider =
                ingredientObject.GetComponent<Collider>();

            if (ingredientCollider != null)
            {
                ingredientCollider.enabled = true;
            }

            itemHolder.ClearHeldItem();

            currentIngredient = ingredient;

            return;
        }

        // PLAYER IS NOT HOLDING ANYTHING

        if (currentIngredient == null)
        {
            return;
        }

        // PICK UP PREPARED VEGETABLE
        if (currentIngredient.IsPrepared)
        {
            GameObject ingredientObject =
                currentIngredient.gameObject;

            ingredientObject.transform.SetParent(
                null
            );

            Collider ingredientCollider =
                ingredientObject.GetComponent<Collider>();

            if (ingredientCollider != null)
            {
                ingredientCollider.enabled = false;
            }

            itemHolder.HoldItem(
                ingredientObject
            );

            currentIngredient = null;

            return;
        }

        // START PREPARATION
        StartCoroutine(
            PrepareIngredient()
        );
    }

    private IEnumerator PrepareIngredient()
    {
        isPreparing = true;

        remainingPreparationTime =
            preparationTime;

        while (remainingPreparationTime > 0f)
        {
            remainingPreparationTime -=
                Time.deltaTime;

            yield return null;
        }

        remainingPreparationTime = 0f;

        if (currentIngredient != null)
        {
            currentIngredient.Prepare();
        }

        isPreparing = false;
    }
}