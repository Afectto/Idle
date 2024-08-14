using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot prefabItemSlot;
    [SerializeField] private UI_Item prefabItemPrefab;
    [SerializeField] private GameObject content;
    private Inventory _inventory;
    private const int SlotCount = 5;
    private void Awake()
    {
        _inventory = new Inventory();
        CreateInventory();
    }

    public void CreateInventory()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            var slot = Instantiate(prefabItemSlot, content.transform);
        }
    }
}
