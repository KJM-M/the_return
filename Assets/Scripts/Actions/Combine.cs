using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Combine")]
public class Combine : Action
{
    [Serializable]
    public class Combination
    {
        public Item firstItem;
        public Item secondItem;
        public Item resultItem;

        [TextArea]
        public string response;

        public Sprite interactionImage;
        public float interactionImageDuration = 2f;

        public List<Connection> connectionsToDisable = new List<Connection>();
        public List<Connection> connectionsToEnable = new List<Connection>();
    }

    public override void RespondToInput(GameController controller, string noun)
    {
        foreach (Combine.Combination combination in controller.combinations)
        {
            if (combination.firstItem == null ||
                combination.secondItem == null ||
                combination.resultItem == null)
                continue;

            string firstOrder =
                combination.firstItem.itemName + " " +
                combination.secondItem.itemName;

            string secondOrder =
                combination.secondItem.itemName + " " +
                combination.firstItem.itemName;

            if (!noun.Equals(firstOrder, StringComparison.OrdinalIgnoreCase) &&
                !noun.Equals(secondOrder, StringComparison.OrdinalIgnoreCase))
                continue;

            List<Item> inventory = controller.player.inventory;

            if (!inventory.Contains(combination.firstItem) ||
                !inventory.Contains(combination.secondItem) ||
                !combination.firstItem.itemEnabled ||
                !combination.secondItem.itemEnabled)
            {
                controller.DisplayActionResponse(
                    "You don't have both items"
                );
                return;
            }

            inventory.Remove(combination.firstItem);
            inventory.Remove(combination.secondItem);

            combination.firstItem.itemEnabled = false;
            combination.secondItem.itemEnabled = false;

            combination.resultItem.itemEnabled = true;
            inventory.Add(combination.resultItem);

            foreach (Connection connection in combination.connectionsToDisable)
            {
                if (connection != null)
                    connection.connectionEnabled = false;
            }

            foreach (Connection connection in combination.connectionsToEnable)
            {
                if (connection != null)
                    connection.connectionEnabled = true;
            }

            string response = string.IsNullOrWhiteSpace(combination.response)
                ? "You combine the items."
                : combination.response;

            Interaction interaction = new Interaction
            {
                response = response,
                interactionImage = combination.interactionImage,
                interactionImageDuration = combination.interactionImageDuration
            };

            controller.DisplayInteraction(interaction);
            return;
        }

        controller.DisplayActionResponse(
            "Those items cannot be combined"
        );
    }
}