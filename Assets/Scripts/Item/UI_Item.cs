using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    [SerializeField] private Item currentItem;
    [SerializeField] private Image skin;
    private ItemSlot _itemSlot;
    
    private void Awake()
    {
        // skin.sprite = currentItem.GetSkin();
    }

    public void SetItem(Item item)
    {
        currentItem = item;
        skin.sprite = currentItem.GetSkin();
    }

    public Item GetItem()
    {
        return currentItem;
    }
}
