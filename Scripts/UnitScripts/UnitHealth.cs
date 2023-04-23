using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(UnitUIManager))]
public class UnitHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float health = 0f;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider unitFrame;
    [SerializeField] UnityEvent<float> onHealthChanged;
    private Transform canvasTransform;

    void Awake()
    {
        canvasTransform = GetComponentInChildren<Canvas>().transform;
    }

    void Start()
    {
        //TODO - nur für demo
        health = maxHealth / 2f;
        onHealthChanged?.Invoke(health/maxHealth);
    }


    public bool isDead()
    {
        return health <= 0;
    }

    private void LateUpdate()
    {
        canvasTransform?.LookAt(canvasTransform.position + Camera.main.transform.forward);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health-Math.Abs(damage));
        if(health <=0)
        {
            Die();
        }
        onHealthChanged?.Invoke(health/maxHealth);
    }

    private void Die()
    {
        //TODO - implementieren
        gameObject.SetActive(false);
    }

    public void Heal(float healing)
    {
        health = Mathf.Min(maxHealth, health + Math.Abs(healing));
        onHealthChanged?.Invoke(health/maxHealth);
    }
}
