using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot prefabItemSlot;
    [SerializeField] private UI_Item prefabItemPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private InventoryManager manager;
    private Inventory _inventory;
    private const int SlotCount = 5;
    private List<ItemSlot> _itemSlots;
    private Enemy _enemy;
    // [SerializeField] private Item item; //DEBUG
    
    private void Awake()
    {
        _inventory = new Inventory();
        _itemSlots = new List<ItemSlot>();
        manager.SetInventory(_inventory);
        CreateInventory();
        _enemy = FindObjectOfType<Enemy>(true);
        _enemy.OnDropItem += AddItem;
    }

    private void Update()
    {
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     AddItem(item);
        // } //DEBUG
    }

    public void CreateInventory()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            _itemSlots.Add(Instantiate(prefabItemSlot, content.transform));
        }
        manager.LoadInventory();
        var itemList = _inventory.GetItemList();

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i])
            {
                AddItemByLoad(itemList[i], i);
            }
        }
    }

    private void AddItemByLoad(Item item, int index)
    {                
        var itemPref = Instantiate(prefabItemPrefab, _itemSlots[index].transform);
        itemPref.SetItem(item, item.amount);
    }

    public void AddItem(Item item)
    {
        if (item.ItemStats.IsStackable)
        {
            for (int i = 0; i < _itemSlots.Count; i++)
            {
                var existingItemUI = _itemSlots[i].GetComponentInChildren<UI_Item>();
                if (existingItemUI != null && existingItemUI.GetItem().ItemStats == item.ItemStats)
                {
                    existingItemUI.IncreaseQuantity();
                    manager.SaveInventory();
                    return;
                }
            }
        }
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].transform.childCount == 0)
            {
                var itemPref = Instantiate(prefabItemPrefab, _itemSlots[i].transform);
                itemPref.SetItem(item);
                _inventory.SetItemToIndex(item, i);
                manager.SaveInventory();
                return;
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            var item = _itemSlots[i].GetComponentInChildren<UI_Item>();
            // ReSharper disable once Unity.NoNullPropagation
            _inventory.SetItemToIndex(item?.GetItem(), i);
        }
        manager.SaveInventory();
    }

    private void OnDestroy()
    {
        _enemy.OnDropItem -= AddItem;
    }
}
