using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Read")]
public class Read : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (ReadItems(controller, controller.player.currentLocation.items, noun))
            return;

        controller.DisplayActionResponse("You cannot read " + noun);
    }

    private bool ReadItems(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemName == noun)
            {
                if (controller.player.CanReadItem(controller, item))
                {
                    if (item.InteractWith(controller, "read"))
                        return true;
                }

                controller.DisplayActionResponse("The " + noun + " does nothing");
                return true;
            }
        }

        return false;
    }
}
