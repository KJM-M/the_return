using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Help")]
public class Help : Action
{
    public override void RespondToInput(GameController controller, string noun)
    {
        string response = "<color=#8FA6C9><b>COMMANDS</b></color>\n";
        response += "Type a verb, followed by a noun when needed.\n\n";

        response += "<color=#8FA6C9>go</color> [direction] — Travel in a direction.\n";
        response += "<color=#8FA6C9>examine</color> [item] — Look at something more closely.\n";
        response += "<color=#8FA6C9>get</color> [item] — Pick up an item.\n";
        response += "<color=#8FA6C9>use</color> [item] — Use an item.\n";
        response += "<color=#8FA6C9>combine</color> [item] [item] — Combine two items.\n";
        response += "<color=#8FA6C9>give</color> [item] — Offer an item to someone.\n";
        response += "<color=#8FA6C9>read</color> [item] — Read writing or inscriptions.\n";
        response += "<color=#8FA6C9>talkto</color> [character] — Speak to someone.\n";
        response += "<color=#8FA6C9>say</color> [word] — Say a word aloud.\n\n";

        response += "<color=#8FA6C9>exitgame</color> — Exit the game.\n\n";

        response += "<color=#aaccaaff>Examples: go north, examine staff, combine lantern matchbox</color>";

        controller.DisplayActionResponse(response);
    }
}
