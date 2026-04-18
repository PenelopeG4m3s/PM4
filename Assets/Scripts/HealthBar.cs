using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    private Health health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = transform.parent.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawHealthBar();
    }

    void DrawHealthBar()
    {
        healthBar.fillAmount = health.currentHealth / health.maxHealth;
    }
}
