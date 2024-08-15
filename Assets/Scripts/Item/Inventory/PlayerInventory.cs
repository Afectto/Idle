using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryManager manager;
    [SerializeField] private UI_Item prefabItemInSlot;
    [SerializeField] private Player player;
    [Space(5)]
    
    [SerializeField] private PlayerItemSlot helmet;
    [SerializeField] private PlayerItemSlot armor;
    [SerializeField] private PlayerItemSlot leg;
    [SerializeField] private PlayerItemSlot weapon;

    private void Start()
    {
        manager.SetPlayerInventory(this);
        
        helmet.ItemApply += OnItemApply;
        helmet.ItemRemove += OnItemApply;
        
        armor.ItemApply += OnItemApply;
        armor.ItemRemove += OnItemApply;
        
        leg.ItemApply += OnItemApply;
        leg.ItemRemove += OnItemApply;
        
        weapon.ItemApply += OnItemApply;
        weapon.ItemRemove += OnItemApply;
        
        manager.LoadPlayerInventory();
    }

    public List<PlayerItemSlot> GetAllItems()
    {
        return new List<PlayerItemSlot> {helmet, armor, leg, weapon};
    }

    public void AddItemInSlot(Item item, string itemName)
    {
        switch (itemName)
        {
            case "helmet" :
                CreateItem(item, helmet.transform);
                helmet.OnApplyItem();
                break;
            case "armor" :
                CreateItem(item, armor.transform);
                armor.OnApplyItem();
                break;
            case "leg" :
                CreateItem(item, leg.transform);
                leg.OnApplyItem();
                break;
            case "weapon" :
                CreateItem(item, weapon.transform);
                weapon.OnApplyItem();
                break;
        }
    }

    private void CreateItem(Item item, Transform transformSlot)
    {
        if (item && !transformSlot.GetComponent<UI_Item>())
        {
            var uiItem = Instantiate(prefabItemInSlot, transformSlot);
            uiItem.SetItem(item);
        }
    }
    
    private void OnItemApply(Item obj)
    {
        manager.SavePlayerInventory();
    }

    private void OnDestroy()
    {
        helmet.ItemApply -= OnItemApply;
        helmet.ItemRemove -= OnItemApply;
        
        armor.ItemApply -= OnItemApply;
        armor.ItemRemove -= OnItemApply;
        
        leg.ItemApply -= OnItemApply;
        leg.ItemRemove -= OnItemApply;
        
        weapon.ItemApply -= OnItemApply;
        weapon.ItemRemove -= OnItemApply;
    }
}
