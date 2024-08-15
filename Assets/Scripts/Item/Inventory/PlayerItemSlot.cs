using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerItemSlot : ItemSlot
{
    [SerializeField] private List<WearableItemsType> possibleItemInSlot;
    private Player _player;
    public event Action<Item> ItemApply;
    public event Action<Item> ItemRemove;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        UI_Item draggableItem = dropped.GetComponent<UI_Item>();
        bool canPlace = false;
        foreach (var type in possibleItemInSlot)
        {
            if (draggableItem.GetItem().ItemStats.WearableType == type)
            {
                canPlace = true;
            }
        }

        if (canPlace)
        {
            base.OnDrop(eventData);
        }
    }

    public void OnApplyItem()
    {
        var itemInSlot = GetComponentInChildren<UI_Item>();
        
        if (itemInSlot != null)
        {
            itemInSlot.GetItem().ApplyItem(_player);
            ItemApply?.Invoke(itemInSlot.GetItem());
        }
    }
    
    public void OnRemoveItem(UI_Item item)
    {
        if (item != null)
        {
            item.GetItem().RemoveItem(_player);
            ItemRemove?.Invoke(item.GetItem());
        }
    }
}
