using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Get")]
public class Get : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        foreach (Item item in controller.player.currentLocation.items)
        {
            if (item.itemEnabled && item.itemName == noun)
            {
                if (item.playerCanTake)
                {
                    controller.player.inventory.Add(item);
                    controller.player.currentLocation.items.Remove(item);

                    if (!item.InteractWith(controller, "get"))
                    {
                        controller.DisplayActionResponse("You take the " + noun);
                    }

                    return;
                }
            }
        }

        controller.DisplayActionResponse("You can't get that");
    }
}
