using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [Header("References")]
    public Slider slider;               
    public PlayerHealth playerHealth;    

    void Start()
    {

        slider.maxValue = playerHealth.GetMaxHealth();
        slider.value = playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        
        slider.value = playerHealth.GetCurrentHealth();
    }
}
