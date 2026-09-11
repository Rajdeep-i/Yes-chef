using System.Collections;
using UnityEngine;

public class PreparationTable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform prepPoint;
    [SerializeField] private float preparationTime = 2f;

    private Ingredient currentIngredient;
    private bool isPreparing;

    public void Interact()
    {
        if (isPreparing)
        {
            Debug.Log("Ingredient is already being prepared.");
            return;
        }

        PlayerItemHolder itemHolder = FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            Debug.LogWarning("PlayerItemHolder not found.");
            return;
        }

        // PLAYER IS HOLDING SOMETHING
        if (itemHolder.IsHoldingItem)
        {
            GameObject ingredientObject = itemHolder.GetHeldItem();

            Ingredient ingredient = ingredientObject.GetComponent<Ingredient>();

            if (ingredient == null)
            {
                Debug.LogWarning("Held object is not an Ingredient.");
                return;
            }

            if (ingredient.IsPrepared)
            {
                Debug.Log("This ingredient is already prepared.");
                return;
            }

            // Place raw ingredient on table
            ingredientObject.transform.SetParent(prepPoint);
            ingredientObject.transform.localPosition = Vector3.zero;
            ingredientObject.transform.localRotation = Quaternion.identity;

            Collider ingredientCollider = ingredientObject.GetComponent<Collider>();

            if (ingredientCollider != null)
            {
                ingredientCollider.enabled = true;
            }

            itemHolder.ClearHeldItem();

            currentIngredient = ingredient;

            Debug.Log("Ingredient placed on the Preparation Table.");

            return;
        }

        // PLAYER IS NOT HOLDING ANYTHING
        if (currentIngredient == null)
        {
            Debug.Log("There is no ingredient on the Preparation Table.");
            return;
        }

        // INGREDIENT IS ALREADY PREPARED → PICK IT UP
        if (currentIngredient.IsPrepared)
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

            Debug.Log("Prepared ingredient picked up from the Preparation Table.");

            return;
        }

        // INGREDIENT IS RAW → START PREPARATION
        StartCoroutine(PrepareIngredient());
    }

    private IEnumerator PrepareIngredient()
    {
        isPreparing = true;

        Debug.Log("Preparing " + currentIngredient.Type + "...");

        yield return new WaitForSeconds(preparationTime);

        currentIngredient.Prepare();

        Debug.Log(currentIngredient.Type + " preparation complete!");

        isPreparing = false;
    }
}