using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Go")]
public class Go : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        if (controller.player.ChangeLocation(controller, noun))
        {
            controller.DisplayLocation();
        }
        else
        {
            controller.DisplayActionResponse("You can't go that way.");
        }
    }
}
