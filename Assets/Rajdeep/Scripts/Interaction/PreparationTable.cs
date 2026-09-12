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
            Debug.Log(
                "Vegetable is already being prepared."
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

        // PLAYER IS HOLDING AN ITEM
        if (itemHolder.IsHoldingItem)
        {
            // Table can only hold one ingredient
            if (currentIngredient != null)
            {
                Debug.Log(
                    "Preparation Table is already occupied."
                );

                return;
            }

            GameObject ingredientObject =
                itemHolder.GetHeldItem();

            Ingredient ingredient =
                ingredientObject.GetComponent<Ingredient>();

            if (ingredient == null)
            {
                Debug.LogWarning(
                    "Held object is not an Ingredient."
                );

                return;
            }

            // Only vegetables can be prepared
            if (ingredient.Type !=
                IngredientType.Vegetable)
            {
                Debug.Log(
                    ingredient.Type +
                    " cannot be prepared on the Table."
                );

                return;
            }

            if (ingredient.IsPrepared)
            {
                Debug.Log(
                    "This vegetable is already prepared."
                );

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

            Debug.Log(
                "Vegetable placed on the Preparation Table."
            );

            return;
        }

        // PLAYER IS NOT HOLDING ANYTHING

        if (currentIngredient == null)
        {
            Debug.Log(
                "There is no vegetable on the Preparation Table."
            );

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

            Debug.Log(
                "Prepared vegetable picked up from the Preparation Table."
            );

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

        Debug.Log(
            "Preparing " +
            currentIngredient.Type +
            "..."
        );

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

            Debug.Log(
                currentIngredient.Type +
                " preparation complete!"
            );
        }

        isPreparing = false;
    }
}