using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

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

public class InventoryManager : MonoBehaviour
{
    private string inventoryFilePath = "Assets/SaveFileInventory.txt";
    private string inventoryPlayerFilePath = "Assets/SaveFileInventoryPlayer.txt";
    private Inventory _inventory;
    private PlayerInventory _playerInventory;

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        _playerInventory = inventory;
    }

    public void SavePlayerInventory()
    {
        ItemListPlayerWrapper wrapper = new ItemListPlayerWrapper(_playerInventory);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(inventoryPlayerFilePath, json);
    }

    public void LoadPlayerInventory()
    {
        if (File.Exists(inventoryPlayerFilePath))
        {
            List<Item> loadedItems = Resources.LoadAll<Item>("Items/ItemType").ToList();
            string json = File.ReadAllText(inventoryPlayerFilePath);
            ItemListPlayerWrapper wrapper = JsonUtility.FromJson<ItemListPlayerWrapper>(json);
            
            var loadedItem = loadedItems.Find(obj => obj.name == wrapper.helmet);
            _playerInventory.AddItemInSlot(loadedItem, "helmet");
            loadedItem = loadedItems.Find(obj => obj.name == wrapper.armor);
            _playerInventory.AddItemInSlot(loadedItem, "armor");
            loadedItem = loadedItems.Find(obj => obj.name == wrapper.leg);
            _playerInventory.AddItemInSlot(loadedItem, "leg");
            loadedItem = loadedItems.Find(obj => obj.name == wrapper.weapon);
            _playerInventory.AddItemInSlot(loadedItem, "weapon");
        }
    }

    
    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void SaveInventory()
    {
        List<Item> itemList = _inventory.GetItemList();
        ItemListWrapper wrapper = new ItemListWrapper(itemList);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(inventoryFilePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            List<Item> loadedItems = Resources.LoadAll<Item>("Items/ItemType").ToList();
            string json = File.ReadAllText(inventoryFilePath);
            ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(json);

            for (int i = 0; i < wrapper.items.Count; i++)
            {
                var loadedItem = loadedItems.Find(obj => obj.name == wrapper.items[i]);
                if (loadedItem != null)
                {
                    loadedItem.amount = wrapper.amount[i];
                    _inventory.AddItem(loadedItem);
                }
                else
                {
                    _inventory.AddItem(null);
                }
            }
        }
    }
}
