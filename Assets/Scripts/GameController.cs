using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public Player player;
    public InventoryUI inventoryUI;
    public TMP_InputField textEntryField;

    public TMP_Text historyText;
    public ScrollRect historyScrollRect;

    public TMP_Text currentText;
    public ScrollRect currentScrollRect;

    public TMP_Text titleText;

    public Action[] actions;
    public List<Combine.Combination> combinations = new List<Combine.Combination>();

    public LocationArtController locationArt;

    public Help helpAction;

    [TextArea]
    public string introText;

    private Interaction temporaryInteraction;
    private string lastActionResponse = "";

    private void Start()
    {
        textEntryField.onSubmit.AddListener(_ => TextEntered());

        historyText.text = introText;
        DisplayLocation();
        textEntryField.ActivateInputField();
    }

    public void DisplayLocation()
    {
        lastActionResponse = "";
        titleText.text = player.currentLocation.locationName;

        string description = player.currentLocation.description;
        description += "<color=#8FA6C9>────────────────────────────────────────────────</color>\n";
        description += player.currentLocation.GetConnectionsText() + "\n";
        description += player.currentLocation.GetItemsText();

        currentText.text = description;
        ScrollCurrentToTop();

        locationArt.ShowLocation(player.currentLocation);
    }

    public void DisplayActionResponse(string response)
    {
        DisplayLocation();
        inventoryUI.Refresh();

        currentText.text += "\n" + response;
        lastActionResponse = response;
        ScrollCurrentToBottom();
    }

    public void DisplayInteraction(Interaction interaction)
    {
        SetTemporaryInteraction(interaction);

        bool playImageBeforeResponse =
            interaction.interactionImage != null &&
            interaction.teleportLocation == null;

        if (playImageBeforeResponse)
            PlayInteraction(interaction);

        DisplayActionResponse(interaction.response);

        if (interaction.interactionImage != null && !playImageBeforeResponse)
            PlayInteraction(interaction);

        if (interaction.interactionImage == null)
            CompleteTemporaryInteraction(interaction);
    }


    private void PlayInteraction(Interaction interaction)
    {
        Coroutine routine = locationArt.PlayInteractionImage(
            interaction.interactionImage,
            interaction.interactionImageDuration
        );

        StartCoroutine(
            CompleteInteractionAfterImage(interaction, routine)
        );
    }

    private IEnumerator CompleteInteractionAfterImage(
        Interaction interaction,
        Coroutine routine
    )
    {
        yield return routine;
        CompleteTemporaryInteraction(interaction);
    }

    private void SetTemporaryInteraction(Interaction interaction)
    {
        if (temporaryInteraction != null &&
            temporaryInteraction != interaction)
        {
            foreach (Item item in temporaryInteraction.temporaryItemsToEnable)
            {
                if (item == null || player.inventory.Contains(item))
                    continue;

                if (interaction.temporaryItemsToEnable.Contains(item) ||
                    interaction.itemsToEnable.Contains(item))
                    continue;

                item.itemEnabled = false;
            }

            foreach (Item item in temporaryInteraction.temporaryItemsToDisable)
            {
                if (item == null || player.inventory.Contains(item))
                    continue;

                if (interaction.itemsToDisable.Contains(item) ||
                    interaction.temporaryItemsToDisable.Contains(item))
                    continue;

                item.itemEnabled = true;
            }
        }

        temporaryInteraction =
            interaction.temporaryItemsToEnable.Count > 0 ||
            interaction.temporaryItemsToDisable.Count > 0
                ? interaction
                : null;
    }

    private void CompleteTemporaryInteraction(Interaction interaction)
    {
        if (temporaryInteraction != interaction)
            return;

        foreach (Item item in interaction.temporaryItemsToEnable)
        {
            if (item != null && !player.inventory.Contains(item))
                item.itemEnabled = false;
        }

        foreach (Item item in interaction.temporaryItemsToDisable)
        {
            if (item != null && !player.inventory.Contains(item))
                item.itemEnabled = true;
        }

        temporaryInteraction = null;
        locationArt.RefreshItemImages();
    }


    public void TextEntered()
    {
        ProcessInput(textEntryField.text);
        textEntryField.text = "";
        textEntryField.ActivateInputField();
    }

    private void LogCommand(
        string previousText,
        string previousResponse,
        bool locationChanged
    )
    {
        string textToLog = locationChanged
            ? previousText
            : previousResponse;

        if (!string.IsNullOrWhiteSpace(textToLog))
        {
            historyText.text += "\n";
            historyText.text += textToLog;
        }

        historyText.text += "\n";
        historyText.text +=
            "<color=#aaccaaff>" + textEntryField.text + "</color>";

        Canvas.ForceUpdateCanvases();

        if (historyScrollRect != null)
            historyScrollRect.verticalNormalizedPosition = 0f;
    }

    public void ScrollCurrentToBottom()
    {
        Canvas.ForceUpdateCanvases();

        if (currentScrollRect != null)
            currentScrollRect.verticalNormalizedPosition = 0f;
    }

    private void ScrollCurrentToTop()
    {
        Canvas.ForceUpdateCanvases();

        if (currentScrollRect != null)
            currentScrollRect.verticalNormalizedPosition = 1f;
    }

    private void ProcessInput(string input)
    {
        input = input.ToLower().Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            DisplayActionResponse("Nothing happens!");
            return;
        }

        string[] words = input.Split(
            new[] { ' ' },
            System.StringSplitOptions.RemoveEmptyEntries
        );

        string keyword = words[0];
        string noun = words.Length > 1
            ? string.Join(" ", words, 1, words.Length - 1)
            : "";

        foreach (Action action in actions)
        {
            if (action.keyword.ToLower() == keyword)
            {
                Location previousLocation = player.currentLocation;
                string previousText = currentText.text;
                string previousResponse = lastActionResponse;

                action.RespondToInput(this, noun);

                LogCommand(
                    previousText,
                    previousResponse,
                    player.currentLocation != previousLocation
                );

                return;
            }
        }

        DisplayActionResponse("Nothing happens!");
    }


    public void ShowHelp()
    {
        helpAction.RespondToInput(this, "");
    }
}
