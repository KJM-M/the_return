using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Exit Game")]
public class ExitGame : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        Application.Quit();
    }
}