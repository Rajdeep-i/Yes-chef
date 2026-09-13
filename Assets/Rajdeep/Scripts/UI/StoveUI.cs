using TMPro;
using UnityEngine;

public class StoveUI : MonoBehaviour
{
    [SerializeField] private GameObject stoveTimerPanel;
    [SerializeField] private TMP_Text stoveSlot1Text;
    [SerializeField] private TMP_Text stoveSlot2Text;

    private Stove stove;

    private void Start()
    {
        stove =
            FindFirstObjectByType<Stove>();

        if (stove == null)
        {
            return;
        }

        if (stoveTimerPanel != null)
        {
            stoveTimerPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (stove == null ||
            stoveTimerPanel == null ||
            stoveSlot1Text == null ||
            stoveSlot2Text == null)
        {
            return;
        }

        bool slot1Cooking =
            stove.IsCooking1;

        bool slot2Cooking =
            stove.IsCooking2;

        // Show or hide the entire panel
        if (slot1Cooking || slot2Cooking)
        {
            stoveTimerPanel.SetActive(true);
        }
        else
        {
            stoveTimerPanel.SetActive(false);
        }

        // Slot 1
        if (slot1Cooking)
        {
            stoveSlot1Text.gameObject.SetActive(true);

            stoveSlot1Text.text =
                "Slot 1: Cooking... " +
                stove.RemainingCookingTime1
                    .ToString("F1") +
                "s";
        }
        else
        {
            stoveSlot1Text.gameObject.SetActive(false);
        }

        // Slot 2
        if (slot2Cooking)
        {
            stoveSlot2Text.gameObject.SetActive(true);

            stoveSlot2Text.text =
                "Slot 2: Cooking... " +
                stove.RemainingCookingTime2
                    .ToString("F1") +
                "s";
        }
        else
        {
            stoveSlot2Text.gameObject.SetActive(false);
        }
    }
}