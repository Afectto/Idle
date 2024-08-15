using System.Collections.Generic;

public class Inventory
{
    private List<Item> _itemsList;
    public List<Item> GetItemList() => _itemsList;

    public Inventory()
    {
        _itemsList = new List<Item>();
    }

    public void SetItemToIndex(Item item, int index)
    {
        _itemsList[index] = item;
    }

    public void AddItem(Item item)
    {
        _itemsList.Add(item);
    }
}
