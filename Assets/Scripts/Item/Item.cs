using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "GameInfo/ItemStats/New Item")]
public class Item :ScriptableObject
{
    [SerializeField] private Image skin;
    [SerializeReference] private ItemStats _stats;
}