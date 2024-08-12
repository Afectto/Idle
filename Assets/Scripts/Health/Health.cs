using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private Text textHealth;
    [SerializeField] private Image fill;

    protected void ChangeSlider(float currentHealth, float maxHealth)
    {        
        textHealth.text = Mathf.FloorToInt(currentHealth) + " / " + Mathf.FloorToInt(maxHealth);
        fill.fillAmount = currentHealth / maxHealth;
    }
}
