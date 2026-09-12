using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private CustomerWindow[] customerWindows;

    [SerializeField] private int minimumIngredients = 2;
    [SerializeField] private int maximumIngredients = 3;

    private void Start()
    {
        GenerateOrdersForAllWindows();
    }

    private void GenerateOrdersForAllWindows()
    {
        foreach (CustomerWindow window in customerWindows)
        {
            if (window == null)
            {
                continue;
            }

            OrderData order = GenerateOrder();

            // Record when this order became active
            order.activationTime = Time.time;

            window.SetOrder(order);
        }
    }

    public OrderData GenerateOrder()
    {
        OrderData order = new OrderData();

        int ingredientCount = Random.Range(
            minimumIngredients,
            maximumIngredients + 1
        );

        for (int i = 0; i < ingredientCount; i++)
        {
            IngredientType randomIngredient =
                (IngredientType)Random.Range(
                    0,
                    System.Enum.GetValues(
                        typeof(IngredientType)
                    ).Length
                );

            order.requiredIngredients.Add(
                randomIngredient
            );
        }

        return order;
    }

    public void GenerateNewOrder(CustomerWindow window)
    {
        if (window == null)
        {
            return;
        }

        OrderData newOrder = GenerateOrder();

        // Record when the new order became active
        newOrder.activationTime = Time.time;

        window.SetOrder(newOrder);

        Debug.Log(
            window.gameObject.name +
            " received a new order."
        );
    }
}