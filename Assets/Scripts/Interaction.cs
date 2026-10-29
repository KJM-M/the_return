using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Interaction
{
    public Action action;

    [TextArea]
    public string response;

    public Sprite interactionImage;
    public float interactionImageDuration = 2f;
    public Sprite locationImageAfterInteraction;

    public string textToMatch;

    public List<Item> itemsToDisable = new List<Item>();
    public List<Item> itemsToEnable = new List<Item>();
    public List<Item> temporaryItemsToEnable = new List<Item>();
    public List<Item> temporaryItemsToDisable = new List<Item>();

    public List<Connection> connectionsToDisable = new List<Connection>();
    public List<Connection> connectionsToEnable = new List<Connection>();
    public List<Connection> connectionsToUnlock = new List<Connection>();

    public List<Item> itemsToAddToInventory = new List<Item>();

    public Location teleportLocation = null;
}
