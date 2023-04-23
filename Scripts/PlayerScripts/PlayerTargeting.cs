using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUIManager))]
public class PlayerTargeting : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public UnityEvent<Transform> onTargetChanged;

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }
    }

    public void AcquireTarget(Transform target)
    {
        ChangeTarget(target);
    }

    private void ChangeTarget(Transform target)
    {
        this.target = target;
        onTargetChanged?.Invoke(target);
    }

    public void ClearTarget()
    {
        ChangeTarget(null);
    }
}
