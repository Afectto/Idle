using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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
        _rectTransform.SetParent(GetComponentInParent<Canvas>().transform);
        _rectTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
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
