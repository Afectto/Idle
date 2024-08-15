using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        parentAfterDrag = transform.parent;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        var playerItemSlot = _rectTransform.parent.GetComponent<PlayerItemSlot>();
        UI_Item uiItem = null;
        if (playerItemSlot)
        {
            uiItem = playerItemSlot.GetComponentInChildren<UI_Item>();
        }
        _rectTransform.SetParent(GetComponentInParent<Canvas>().transform);
        _rectTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;

        if (playerItemSlot)
        {
            playerItemSlot.OnRemoveItem(uiItem);
        }

        parentAfterDrag.GetComponentInParent<UI_Inventory>()?.UpdateUI();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
        var playerItemSlot = _rectTransform.parent.GetComponent<PlayerItemSlot>();
        if (playerItemSlot)
        {
            playerItemSlot.OnApplyItem();
        }
        GetComponentInParent<UI_Inventory>()?.UpdateUI();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform.parent as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out localPoint
        );

        _rectTransform.anchoredPosition = localPoint;
    }
}
