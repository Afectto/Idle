[System.Serializable]
public class ItemListPlayerWrapper
{
    public string helmet;
    public string armor;
    public string leg;
    public string weapon;

    public ItemListPlayerWrapper(PlayerInventory inventory)
    {
        var items = inventory.GetAllItems();
        if (items.Count < 4) return;
        helmet = items[0].GetComponentInChildren<UI_Item>()?.GetItem().name;
        armor = items[1].GetComponentInChildren<UI_Item>()?.GetItem().name;
        leg = items[2].GetComponentInChildren<UI_Item>()?.GetItem().name;
        weapon = items[3].GetComponentInChildren<UI_Item>()?.GetItem().name;
    }
}