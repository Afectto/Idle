using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    [SerializeField] private Item currentItem;
    [SerializeField] private Image skin;
    [SerializeField] private TextMeshProUGUI text;
    private int _amount;
    private ItemSlot _itemSlot;
    private float _lastClickTime;
    private const float DoubleClickDelay = 0.3f;

    private void Awake()
    {
        _amount = 1;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем левую кнопку мыши
        {
            if (Time.time - _lastClickTime <= DoubleClickDelay)
            {
                TryConsumeItem();
            }
            _lastClickTime = Time.time;
        }
    }

    private void TryConsumeItem()
    {
        if (currentItem.ItemStats.StatsType == StatType.Restored)
        {
            ReduceQuantity();
            if (_amount <= 0)
            {
                Destroy(gameObject);
            }
            currentItem.ApplyItem(FindObjectOfType<Player>());
            GetComponentInParent<UI_Inventory>()?.UpdateUI();
        }
    }
    
    public void SetItem(Item item)
    {
        currentItem = item;
        skin.sprite = currentItem.GetSkin();
        UpdateText();
    }

    public void SetItem(Item item, int amount)
    {
        currentItem = item;
        skin.sprite = currentItem.GetSkin();
        _amount = amount;
        UpdateText();
    }
    
    public Item GetItem()
    {
        return currentItem;
    }

    public void IncreaseQuantity()
    {
        _amount++;
        UpdateText();
    }

    public void ReduceQuantity()
    {
        _amount--;
        UpdateText();
    }

    private void UpdateText()
    {
        currentItem.amount = _amount;
        text.gameObject.SetActive(_amount > 1);
        text.text = _amount.ToString();
    }
}
