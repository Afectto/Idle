using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPlayerButton : MonoBehaviour
{
    private Button _button;
    private Player _player;
    
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
        _player = GetComponentInParent<Player>();
        _player.OnChangeState += OnChangeState;
    }

    private void OnClickButton()
    {
        _player.GetComponent<Health>().Heal(_player.GetCurrentStats().Health);
    }
    
    private void OnChangeState(State obj)
    {
        gameObject.SetActive(obj.GetType().Name == nameof(OutOfCombatState));
    }
}
