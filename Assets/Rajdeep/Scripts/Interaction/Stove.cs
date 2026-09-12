using System.Collections;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform stovePoint1;
    [SerializeField] private Transform stovePoint2;

    [SerializeField] private float cookingTime = 6f;

    private Ingredient ingredient1;
    private Ingredient ingredient2;

    private float remainingCookingTime1;
    private float remainingCookingTime2;

    public bool IsCooking1 =>
        ingredient1 != null &&
        !ingredient1.IsCooked;

    public bool IsCooking2 =>
        ingredient2 != null &&
        !ingredient2.IsCooked;

    public float RemainingCookingTime1 =>
        remainingCookingTime1;

    public float RemainingCookingTime2 =>
        remainingCookingTime2;

    public void Interact()
    {
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

            // Only Meat can be cooked
            if (ingredient.Type != IngredientType.Meat)
            {
                Debug.Log(
                    ingredient.Type +
                    " cannot be cooked on the Stove."
                );

                return;
            }

            // Already cooked meat does not need to go
            // back onto the stove
            if (ingredient.IsCooked)
            {
                Debug.Log(
                    "This meat is already cooked."
                );

                return;
            }

            // Try Slot 1
            if (ingredient1 == null)
            {
                PlaceIngredientInSlot(
                    ingredientObject,
                    ingredient,
                    stovePoint1,
                    1
                );

                return;
            }

            // Try Slot 2
            if (ingredient2 == null)
            {
                PlaceIngredientInSlot(
                    ingredientObject,
                    ingredient,
                    stovePoint2,
                    2
                );

                return;
            }

            // Both slots are occupied
            Debug.Log(
                "Both Stove slots are occupied."
            );

            return;
        }

        // PLAYER IS NOT HOLDING ANYTHING

        // Pick up cooked meat from Slot 1
        if (ingredient1 != null &&
            ingredient1.IsCooked)
        {
            PickUpIngredient(
                ingredient1,
                1,
                itemHolder
            );

            return;
        }

        // Pick up cooked meat from Slot 2
        if (ingredient2 != null &&
            ingredient2.IsCooked)
        {
            PickUpIngredient(
                ingredient2,
                2,
                itemHolder
            );

            return;
        }

        Debug.Log(
            "No cooked meat is ready on the Stove."
        );
    }

    private void PlaceIngredientInSlot(
        GameObject ingredientObject,
        Ingredient ingredient,
        Transform stovePoint,
        int slot
    )
    {
        ingredientObject.transform.SetParent(
            stovePoint
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

        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder != null)
        {
            itemHolder.ClearHeldItem();
        }

        if (slot == 1)
        {
            ingredient1 = ingredient;
            remainingCookingTime1 = cookingTime;

            StartCoroutine(
                CookIngredient(
                    ingredient,
                    1
                )
            );
        }
        else
        {
            ingredient2 = ingredient;
            remainingCookingTime2 = cookingTime;

            StartCoroutine(
                CookIngredient(
                    ingredient,
                    2
                )
            );
        }

        Debug.Log(
            "Meat placed in Stove Slot " +
            slot +
            ". Cooking for " +
            cookingTime +
            " seconds."
        );
    }

    private IEnumerator CookIngredient(
        Ingredient ingredient,
        int slot
    )
    {
        while (true)
        {
            if (slot == 1)
            {
                remainingCookingTime1 -=
                    Time.deltaTime;

                if (remainingCookingTime1 <= 0f)
                {
                    remainingCookingTime1 = 0f;
                    break;
                }
            }
            else
            {
                remainingCookingTime2 -=
                    Time.deltaTime;

                if (remainingCookingTime2 <= 0f)
                {
                    remainingCookingTime2 = 0f;
                    break;
                }
            }

            yield return null;
        }

        if (ingredient == null)
        {
            yield break;
        }

        ingredient.Cook();

        Debug.Log(
            "Stove Slot " +
            slot +
            " cooking complete!"
        );
    }

    private void PickUpIngredient(
        Ingredient ingredient,
        int slot,
        PlayerItemHolder itemHolder
    )
    {
        GameObject ingredientObject =
            ingredient.gameObject;

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

        if (slot == 1)
        {
            ingredient1 = null;
            remainingCookingTime1 = 0f;
        }
        else
        {
            ingredient2 = null;
            remainingCookingTime2 = 0f;
        }

        Debug.Log(
            "Cooked meat picked up from Stove Slot " +
            slot +
            "."
        );
    }
}