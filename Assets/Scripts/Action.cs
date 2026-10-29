using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string keyword;

    public abstract void RespondToInput(GameController controller, string noun);
}
