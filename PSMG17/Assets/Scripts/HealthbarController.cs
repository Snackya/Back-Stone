using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour {

    public Slider HealthSlider;
    public float currentHealth;
    public float maxHealth = 100f;

    void Start () {
        currentHealth = maxHealth;
    }

    void Update()
    {
        HealthSlider.value = CalculateHealth();
    }

    public float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            HealthSlider.value = 0;
            Destroy(gameObject);
        }
    }
}
