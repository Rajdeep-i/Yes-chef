using TMPro;
using UnityEngine;

public class PreparationUI : MonoBehaviour
{
    [SerializeField] private GameObject choppingTimerPanel;
    [SerializeField] private TMP_Text choppingTimerText;

    private PreparationTable preparationTable;

    private void Start()
    {
        preparationTable =
            FindFirstObjectByType<PreparationTable>();

        if (preparationTable == null)
        {
            Debug.LogWarning(
                "PreparationTable not found."
            );

            return;
        }

        if (choppingTimerPanel != null)
        {
            choppingTimerPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (preparationTable == null ||
            choppingTimerPanel == null ||
            choppingTimerText == null)
        {
            return;
        }

        if (preparationTable.IsPreparing)
        {
            choppingTimerPanel.SetActive(true);

            float remainingTime =
                preparationTable.RemainingPreparationTime;

            choppingTimerText.text =
                "Chopping...\n" +
                remainingTime.ToString("F1") +
                "s";
        }
        else
        {
            choppingTimerPanel.SetActive(false);
        }
    }
}