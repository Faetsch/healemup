using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitFrameScript : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] public GameObject unit;
    [SerializeField] public GameObject border;
    private PlayerTargeting playerTargeting;
    private UnitFrameManager unitFrameManager;
    

    private void Start()
    {
        playerTargeting = GameObject.FindAnyObjectByType<PlayerTargeting>();
        unitFrameManager = GameObject.FindAnyObjectByType<UnitFrameManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerTargeting.AcquireTarget(unit.transform);
        unitFrameManager.ClearUnitFrameTarget();
        border.SetActive(true);
    }
}
