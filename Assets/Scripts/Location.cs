using UnityEngine;
using System.Collections.Generic;

public class Location : MonoBehaviour
{
    public string locationName;

    public Sprite locationImage;

    [TextArea]
    public string description;

    public Connection[] connections;
    public List<Item> items = new List<Item>();

    public string GetItemsText()
    {
        string result = "";

        foreach (Item item in items)
        {
            if (item.itemEnabled)
                result += "You see " + item.description + "\n";
        }

        return result;
    }

    public string GetConnectionsText()
    {
        string result = "";

        foreach (Connection connection in connections)
        {
            if (connection.connectionEnabled)
                result += connection.description + "\n";
        }

        return result;
    }

    internal bool HasItem(Item itemToCheck)
    {
        foreach (Item item in items)
        {
            if (item == itemToCheck && item.itemEnabled)
                return true;
        }

        return false;
    }

    public Connection GetConnection(string connectionNoun)
    {
        foreach (Connection connection in connections)
        {
            if (connection.connectionName.ToLower() == connectionNoun.ToLower())
                return connection;
        }

        return null;
    }
}
