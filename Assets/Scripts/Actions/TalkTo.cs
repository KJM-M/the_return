using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/TalkTo")]
public class TalkTo : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (TalkToItem(controller, controller.player.currentLocation.items, noun))
            return;

        controller.DisplayActionResponse("There is no " + noun + " here!");
    }

    private bool TalkToItem(GameController controller, List<Item> items, string noun)
    {
        foreach (Item item in items)
        {
            if (item.itemName == noun && item.itemEnabled)
            {
                if (controller.player.CanTalkToItem(controller, item))
                {
                    if (item.InteractWith(controller, "talkto"))
                        return true;
                }

                controller.DisplayActionResponse("The " + noun + " doesn't respond");
                return true;
            }
        }

        return false;
    }
}
