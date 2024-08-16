using System.Collections.Generic;

[System.Serializable]
public class ItemListWrapper
{
    public List<string > items;
    public List<int> amount;

    public ItemListWrapper(List<Item> itemList)
    {
        items = new List<string>();
        amount = new List<int>();
        foreach (var item in itemList)
        {
            if (item)
            {
                amount.Add(item.amount);
                items.Add(item.name);
            }
            else
            {
                amount.Add(0);
                items.Add("");
            }
        }
    }
}