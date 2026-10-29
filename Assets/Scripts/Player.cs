using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Location currentLocation;
    public List<Item> inventory = new List<Item>();

    public bool ChangeLocation(GameController controller, string connectionNoun)
    {
        Connection connection = currentLocation.GetConnection(connectionNoun);

        if (connection != null &&
            connection.connectionEnabled &&
            !connection.isLocked &&
            (connection.requiredItem == null || HasItem(connection.requiredItem)))
        {
            currentLocation = connection.Location;
            return true;
        }

        return false;
    }

    public void Teleport(GameController controller, Location destination)
    {
        currentLocation = destination;
    }

    internal bool CanTalkToItem(GameController controller, Item item)
    {
        return item.playerCanTalkTo;
    }

    internal bool CanGiveToItem(GameController controller, Item item)
    {
        return item.playerCanGiveTo;
    }

    internal bool CanReadItem(GameController controller, Item item)
    {
        return item.playerCanRead;
    }

    internal bool CanUseItem(GameController controller, Item item)
    {
        if (item.targgetItem == null)
            return true;

        if (HasItem(item.targgetItem))
            return true;

        if (currentLocation.HasItem(item.targgetItem))
            return true;

        return false;
    }

    private bool HasItem(Item itemToCheck)
    {
        foreach (Item item in inventory)
        {
            if (item == itemToCheck && item.itemEnabled)
                return true;
        }

        return false;
    }

    public bool HasItemByName(string noun)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName.ToLower() == noun.ToLower())
                return true;
        }

        return false;
    }
}
