using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightIndicator : MonoBehaviour
{
    [SerializeField] private Image indicator;
    private Character owner;

    private void Awake()
    {
        owner = GetComponentInParent<Character>();
        owner.OnChangeState += OnChangeState;
    }

    private void OnChangeState(State currentState)
    {
        if(currentState == null) return;
        
        StopAllCoroutines();
        
        indicator.gameObject.SetActive(true);
        indicator.fillAmount = 1;
        float time;
        switch (currentState.GetType().Name)
        {
            case nameof(AttackState):
                indicator.color = Color.gray;
                time = owner.GetWeaponStats().AttackDuration;
                StartCoroutine(DecreaseIndicator(time)); 
                break;
            case nameof(PrepareToFightState):
                indicator.color = new Color(0.8867924f, 0.54904f, 0.2216981f, 1);
                time = owner.GetCurrentStats().TimeToPrepareAttack;
                StartCoroutine(DecreaseIndicator(time)); 
                break;
            case nameof(IdleState):
                indicator.gameObject.SetActive(false);
                break;
            case nameof(OutOfCombatState):
                indicator.gameObject.SetActive(false);
                break;
            case nameof(ChangeWeaponState):
                var weaponState = currentState as ChangeWeaponState;
                indicator.color = Color.cyan;
                StartCoroutine(DecreaseIndicator(weaponState.GetChangeDuration())); 
                break;
        }
    }
    
    private IEnumerator DecreaseIndicator(float duration)
    {
        float elapsedTime = 0;
        indicator.fillAmount = 1;
    
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            indicator.fillAmount = Mathf.Lerp(1, 0, elapsedTime / duration);
            yield return null;
        }
    
        indicator.fillAmount = 0; 
    }
}
