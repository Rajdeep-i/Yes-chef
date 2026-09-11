using System.Collections;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform stovePoint;
    [SerializeField] private float cookingTime = 6f;

    private Ingredient currentIngredient;
    private bool isCooking;

    public void Interact()
    {
        if (isCooking)
        {
            Debug.Log("Ingredient is currently cooking.");
            return;
        }

        PlayerItemHolder itemHolder = FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            Debug.LogWarning("PlayerItemHolder not found.");
            return;
        }

        // PLAYER IS HOLDING AN ITEM
        if (itemHolder.IsHoldingItem)
        {
            GameObject ingredientObject = itemHolder.GetHeldItem();

            Ingredient ingredient = ingredientObject.GetComponent<Ingredient>();

            if (ingredient == null)
            {
                Debug.LogWarning("Held object is not an Ingredient.");
                return;
            }

            if (!ingredient.IsPrepared)
            {
                Debug.Log("Ingredient must be prepared before cooking.");
                return;
            }

            if (ingredient.IsCooked)
            {
                Debug.Log("Ingredient is already cooked.");
                return;
            }

            // Place ingredient on stove
            ingredientObject.transform.SetParent(stovePoint);
            ingredientObject.transform.localPosition = Vector3.zero;
            ingredientObject.transform.localRotation = Quaternion.identity;

            Collider ingredientCollider = ingredientObject.GetComponent<Collider>();

            if (ingredientCollider != null)
            {
                ingredientCollider.enabled = true;
            }

            itemHolder.ClearHeldItem();

            currentIngredient = ingredient;

            Debug.Log("Prepared ingredient placed on the Stove.");

            return;
        }

        // PLAYER IS NOT HOLDING ANYTHING

        if (currentIngredient == null)
        {
            Debug.Log("There is no ingredient on the Stove.");
            return;
        }

        // COOKED INGREDIENT → PICK IT UP
        if (currentIngredient.IsCooked)
        {
            GameObject ingredientObject = currentIngredient.gameObject;

            ingredientObject.transform.SetParent(null);

            Collider ingredientCollider = ingredientObject.GetComponent<Collider>();

            if (ingredientCollider != null)
            {
                ingredientCollider.enabled = false;
            }

            itemHolder.HoldItem(ingredientObject);

            currentIngredient = null;

            Debug.Log("Cooked ingredient picked up from the Stove.");

            return;
        }

        // PREPARED INGREDIENT → START COOKING
        StartCoroutine(CookIngredient());
    }

    private IEnumerator CookIngredient()
    {
        isCooking = true;

        Debug.Log("Cooking " + currentIngredient.Type + "...");

        yield return new WaitForSeconds(cookingTime);

        currentIngredient.Cook();

        Debug.Log(currentIngredient.Type + " cooking complete!");

        isCooking = false;
    }
}