using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private Text textHealth;
    [SerializeField] private Image fill;

    private float _maxHealth;
    private float _currentHealth;

    public Action IsDead;
    
    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        ChangeSlider(_currentHealth, _maxHealth);
    }

    public void Heal(float value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        ChangeSlider(_currentHealth, _maxHealth);
    }
    
    public void SetDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        ChangeSlider(_currentHealth, _maxHealth);
    }
    
    protected void ChangeSlider(float maxHealth)
    {
        _maxHealth = maxHealth;
        ChangeSlider(_currentHealth, _maxHealth);
    }
    
    protected void ChangeSlider(float currentHealth, float maxHealth)
    {        
        textHealth.text = Mathf.FloorToInt(currentHealth) + " / " + Mathf.FloorToInt(maxHealth);
        fill.fillAmount = currentHealth / maxHealth;
        if (currentHealth == 0)
        {
            IsDead?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
