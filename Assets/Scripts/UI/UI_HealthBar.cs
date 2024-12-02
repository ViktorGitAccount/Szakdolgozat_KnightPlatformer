using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Core entity;
    private RectTransform rectTransform;
    private Slider slider;



    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        entity = GetComponentInParent<Core>();

        entity.onFlipped += FlipUI;
        entity.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = entity.GetMaxHealthValue();
        slider.value = entity.currentHeatlh;
    }

    private void FlipUI()
    {
        rectTransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        entity.onHealthChanged -= UpdateHealthUI;
    }
}
