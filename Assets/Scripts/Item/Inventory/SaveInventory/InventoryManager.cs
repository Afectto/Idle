using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    private string _inventoryFilePath;
    private string _inventoryPlayerFilePath ;
    private Inventory _inventory;
    private PlayerInventory _playerInventory;

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        _playerInventory = inventory;
        _inventoryPlayerFilePath = Path.Combine(Application.persistentDataPath, "SaveFileInventoryPlayer.txt");
    }

    public void SavePlayerInventory()
    {
        ItemListPlayerWrapper wrapper = new ItemListPlayerWrapper(_playerInventory);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(_inventoryPlayerFilePath, json);
    }

    public void LoadPlayerInventory()
    {
        if (File.Exists(_inventoryPlayerFilePath))
        {
            List<Item> loadedItems = Resources.LoadAll<Item>("Items/ItemType").ToList();
            string json = File.ReadAllText(_inventoryPlayerFilePath);
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
        _inventoryFilePath = Path.Combine(Application.persistentDataPath, "SaveFileInventory.txt");
    }

    public void SaveInventory()
    {
        List<Item> itemList = _inventory.GetItemList();
        ItemListWrapper wrapper = new ItemListWrapper(itemList);
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(_inventoryFilePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(_inventoryFilePath))
        {
            List<Item> loadedItems = Resources.LoadAll<Item>("Items/ItemType").ToList();
            string json = File.ReadAllText(_inventoryFilePath);
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
