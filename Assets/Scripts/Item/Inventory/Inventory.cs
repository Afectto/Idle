using System.Collections.Generic;

public class Inventory
{
    private List<Item> _itemsList;

    public Inventory()
    {
        _itemsList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        _itemsList.Add(item);
    }
}
