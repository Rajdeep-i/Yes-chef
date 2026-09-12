using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderData
{
    public List<IngredientType> requiredIngredients =
        new List<IngredientType>();

    public bool IsCompleted = false;

    // Time when this order became active
    public float activationTime;
}