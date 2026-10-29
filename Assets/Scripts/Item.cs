using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public string itemName;

    [TextArea]
    public string description;

    [Header("Location Image")]
    public Sprite locationSprite;
    public Vector2 locationSpritePosition;
    public Vector2 locationSpriteSize = new Vector2(100f, 100f);
    public float locationSpriteRotation;

    public bool playerCanTake;
    public bool itemEnabled = true;

    public Item targgetItem = null;

    public Interaction[] interactions;

    public List<Item> itemsToDrop = new List<Item>();

    public bool playerCanTalkTo = false;
    public bool playerCanGiveTo = false;
    public bool playerCanRead = false;

    public bool InteractWith(GameController controller, string actionKeyword, string noun = "")
    {
        foreach (Interaction interaction in interactions)
        {
            if (interaction.action.keyword != actionKeyword)
                continue;

            if (noun != "" && noun.ToLower() != interaction.textToMatch.ToLower())
                continue;

            foreach (Item disableItem in interaction.itemsToDisable)
            {
                disableItem.itemEnabled = false;

                foreach (Item droppedItem in disableItem.itemsToDrop)
                {
                    if (droppedItem != null)
                        droppedItem.itemEnabled = true;
                }
            }

            foreach (Item enableItem in interaction.itemsToEnable)
                enableItem.itemEnabled = true;

            foreach (Item inventoryItem in interaction.itemsToAddToInventory)
            {
                if (inventoryItem == null)
                    continue;

                inventoryItem.itemEnabled = true;

                if (!controller.player.inventory.Contains(inventoryItem))
                    controller.player.inventory.Add(inventoryItem);

                controller.player.currentLocation.items.Remove(inventoryItem);
            }
            
            foreach (Item temporaryItem in interaction.temporaryItemsToEnable)
                temporaryItem.itemEnabled = true;

            foreach (Item temporaryItem in interaction.temporaryItemsToEnable)
                temporaryItem.itemEnabled = true;

            foreach (Item temporaryItem in interaction.temporaryItemsToDisable)
                temporaryItem.itemEnabled = false;

            foreach (Connection disableConnection in interaction.connectionsToDisable)
                disableConnection.connectionEnabled = false;

            foreach (Connection enableConnection in interaction.connectionsToEnable)
                enableConnection.connectionEnabled = true;
                
            foreach (Connection unlockConnection in interaction.connectionsToUnlock)
            {
                if (unlockConnection != null)
                    unlockConnection.isLocked = false;
            }

            if (interaction.teleportLocation != null)
            {
                controller.player.Teleport(controller, interaction.teleportLocation);
            }

            if (interaction.locationImageAfterInteraction != null)
            {
                controller.player.currentLocation.locationImage =
                    interaction.locationImageAfterInteraction;
            }
            
            controller.DisplayInteraction(interaction);
            return true;
        }



        return false;
    }
}
