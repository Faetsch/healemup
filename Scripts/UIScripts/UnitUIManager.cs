using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] public Slider healthSlider;
    [SerializeField] public Slider unitFrame;


    public void HealthChanged(float newHealth)
    {
        healthSlider.value = newHealth;
        unitFrame.value = newHealth;
    }

}
