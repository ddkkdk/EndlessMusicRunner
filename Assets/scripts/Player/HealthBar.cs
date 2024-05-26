using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public float maxHealth;
    public float currentHealth;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);

        }
        else 
        {
            Instance = this;
        
        }
        currentHealth = maxHealth;
      //  UIManager.Instance.SetHealthBar(currentHealth, maxHealth);
        
    }

    
    void Update()
    {
        
    }
}
