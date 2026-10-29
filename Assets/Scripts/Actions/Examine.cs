using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Examine")]
public class Examine : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (CheckItems(controller, controller.player.currentLocation.items, noun))
            return;

        if (CheckItems(controller, controller.player.inventory, noun))
            return;

        controller.DisplayActionResponse("You can't examine " + noun);
    }

    private bool CheckItems(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemEnabled && item.itemName == noun)
            {
                if (item.InteractWith(controller, "examine"))
                    return true;

                controller.DisplayActionResponse("You see " + item.description);
                return true;
            }
        }

        return false;
    }
}
