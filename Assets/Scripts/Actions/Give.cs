using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Give")]
public class Give : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (controller.player.HasItemByName(noun))
        {
            if (GiveToItem(controller, controller.player.currentLocation.items, noun))
                return;

            controller.DisplayActionResponse("Nothing takes the " + noun);
            return;
        }

        controller.DisplayActionResponse("You don't have " + noun + " to give");
    }

    private bool GiveToItem(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemEnabled && controller.player.CanGiveToItem(controller, item))
            {
                if (item.InteractWith(controller, "give", noun))
                    return true;
            }
        }

        return false;
    }
}
