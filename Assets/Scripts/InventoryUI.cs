using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Player player;
    public RectTransform slotContainer;
    public GameObject slotPrefab;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = slotContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(slotContainer.GetChild(i).gameObject);
        }

        foreach (Item item in player.inventory)
        {
            if (item == null || !item.itemEnabled)
                continue;

            GameObject slot = Instantiate(slotPrefab, slotContainer);
            TMP_Text itemText = slot.GetComponentInChildren<TMP_Text>();
            itemText.text = item.itemName;
        }
    }
}