using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot prefabItemSlot;
    [SerializeField] private UI_Item prefabItemPrefab;
    [SerializeField] private GameObject content;
    private Inventory _inventory;
    private const int SlotCount = 5;
    [SerializeField]private List<ItemSlot> _itemSlots;
    [SerializeField] private Item Item;
    private void Awake()
    {
        _inventory = new Inventory();
        CreateInventory();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddItem(Item);
        }
    }

    public void CreateInventory()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            _inventory.AddItem(null);
            _itemSlots.Add(Instantiate(prefabItemSlot, content.transform));
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].transform.childCount == 0)
            {
                var itemPref = Instantiate(prefabItemPrefab, _itemSlots[i].transform);
                itemPref.SetItem(item);
                // _itemSlots[i].AddItem(itemPref);
                _inventory.SetItemToIndex(item, i);
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
    }
}
