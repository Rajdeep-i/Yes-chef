using System.Collections;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private CustomerWindow[] customerWindows;
    [SerializeField] private TMP_Text[] orderTexts;

    private IEnumerator Start()
    {
        // Wait one frame for OrderManager
        // to generate the initial orders.
        yield return null;

        RefreshOrders();
    }

    private void Update()
    {
        // Continuously update order timers.
        RefreshOrders();
    }

    public void RefreshOrders()
    {
        if (customerWindows == null ||
            customerWindows.Length == 0)
        {
            Debug.LogWarning(
                "Customer Windows are not assigned to OrderUI."
            );

            return;
        }

        if (orderTexts == null ||
            orderTexts.Length == 0)
        {
            Debug.LogWarning(
                "Order Texts are not assigned to OrderUI."
            );

            return;
        }

        int count = Mathf.Min(
            customerWindows.Length,
            orderTexts.Length
        );

        for (int i = 0; i < count; i++)
        {
            if (customerWindows[i] == null ||
                orderTexts[i] == null)
            {
                continue;
            }

            OrderData order =
                customerWindows[i].GetCurrentOrder();

            // No order
            if (order == null)
            {
                orderTexts[i].text =
                    "WINDOW " + (i + 1) +
                    "\nNo Order";

                continue;
            }

            // Completed order waiting for respawn
            if (order.IsCompleted)
            {
                orderTexts[i].text =
                    "WINDOW " + (i + 1) +
                    "\nCOMPLETED!";

                continue;
            }

            // Calculate how long this order has been open
            float elapsedTime =
                Time.time -
                order.activationTime;

            int elapsedSeconds =
                Mathf.FloorToInt(elapsedTime);

            string orderText =
                "WINDOW " +
                (i + 1) +
                "\n";

            orderText +=
                "Time: " +
                elapsedSeconds +
                "s\n";

            foreach (
                IngredientType ingredient
                in customerWindows[i]
                    .GetRemainingIngredients()
            )
            {
                orderText +=
                    ingredient +
                    "\n";
            }

            orderTexts[i].text =
                orderText;
        }
    }
}