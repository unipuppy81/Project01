using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMana : MonoBehaviour
{
    public Slider manaSlider;
    public float currentMP;
    public float maxMP;


    public TextMeshProUGUI textMP;
    private void Awake()
    {
        currentMP = maxMP;
        UpdateManaBar();
    }
    public void Update()
    {
        if (textMP != null) { textMP.text = $"{currentMP:F0}/{maxMP:F0}"; };
    }
    public void ConsumeMP(float mana)
    {
        currentMP += -mana;

        Debug.Log(currentMP);
        if (IsEmptyMana())
        {
            currentMP = 0.0f;
        }

        UpdateManaBar();
    }
    public void RecoveryMP(float mp)
    {
        currentMP += mp;

        if (IsFullMana()) { 
            currentMP = maxMP;
        }
        UpdateManaBar();
    }

    public void UpdateManaBar()
    {
        manaSlider.value = currentMP / maxMP;
    }
    public bool IsEmptyMana() { return currentMP <= 0.0f; }
    public bool IsFullMana() { return currentMP >= maxMP; }
    public float RemainingAmount() { return Mathf.Clamp01(currentMP / maxMP); }
}
