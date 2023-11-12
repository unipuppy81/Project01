using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float currentHP;
    public float maxHP;

    public TextMeshProUGUI textHP;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }


    public void Update()
    {
        if (textHP != null) { textHP.text = $"{currentHP:F0}/{maxHP:F0}"; };
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;

        if (IsDeath())
            currentHP = 0.0f;

        UpdateHealthBar();
    }

    public void RecoveryHP(float hp)
    {
        currentHP += hp;

        if (IsFullHealth())
            currentHP = maxHP;


        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        healthSlider.value = currentHP / maxHP;
    }

    public bool IsDeath() { return (currentHP <= 0); }
    public bool IsFullHealth() { return (currentHP >= maxHP); }
    public float RemainingAmount() { return Mathf.Clamp01(currentHP / maxHP); }
}
