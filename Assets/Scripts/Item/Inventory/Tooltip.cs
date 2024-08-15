using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{    
    private Camera _camera;
    private TextMeshProUGUI _text;
    private Image _background;

    private void Awake()
    {
        _camera = Camera.main;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _background = GetComponentInChildren<Image>();
        
        HideTooltip();

    }
    
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
                Input.mousePosition, _camera, out var localPoint);
            transform.localPosition = localPoint + (_text.preferredWidth/3) * Vector2.up;
        }
    }
    
    public void ShowTooltip(string text)
    {
        gameObject.SetActive(true);
        _text.text = text;
        
        float textPadding = 4;
        Vector2 backgroundSize = new Vector2(_text.preferredWidth + textPadding * 2f, _text.preferredHeight + textPadding * 2f);
        _background.rectTransform.sizeDelta = backgroundSize;

    }
    
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
