using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // if (transform.childCount == 0)
        // {
        //     var dropped = eventData.pointerDrag;
        //     DragDrop draggableItem = dropped.GetComponent<DragDrop>();
        //     draggableItem.parentAfterDrag = transform;
        // }
        var dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            DragDrop draggableItem = dropped.GetComponent<DragDrop>();
            
            if (transform.childCount == 0)
            {
                // Если ячейка пуста, просто устанавливаем объект
                draggableItem.parentAfterDrag = transform;
            }
            else
            {
                DragDrop itemInCurrentSlot = transform.GetChild(0).GetComponent<DragDrop>();
                
                itemInCurrentSlot.transform.SetParent(draggableItem.parentAfterDrag);
                itemInCurrentSlot.transform.localPosition = Vector3.zero;;
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}
