using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            DragDrop draggableItem = dropped.GetComponent<DragDrop>();
            
            var uiItem = transform.GetComponentInChildren<UI_Item>();
            if (!uiItem)
            {
                draggableItem.parentAfterDrag = transform;
            }
            else
            {
                DragDrop itemInCurrentSlot = uiItem.GetComponent<DragDrop>();
                
                itemInCurrentSlot.transform.SetParent(draggableItem.parentAfterDrag);
                itemInCurrentSlot.transform.localPosition = Vector3.zero;;
                draggableItem.parentAfterDrag = transform;
            }
        }
        GetComponentInParent<UI_Inventory>()?.UpdateUI();
    }

    public void AddItem(UI_Item item)
    {
        item.transform.SetParent(transform);
    }
}
