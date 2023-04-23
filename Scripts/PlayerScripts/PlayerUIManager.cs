using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] public TMP_Text targetText;
    [SerializeField] public List<GameObject> unitFrames;
    [SerializeField] public GameObject castingBarPanel;
    private TMP_Text castingBarText;
    private Slider castingBarSlider;


    private void Awake()
    {
        castingBarSlider = castingBarPanel.GetComponentInChildren<Slider>();
        castingBarText = castingBarPanel.GetComponentInChildren<TMP_Text>();
    }

    public void ChangeTargetText(Transform target)
    {
        targetText.text = target != null ? target.name : string.Empty;
        if (targetText.text == string.Empty)
        {
            ClearUnitFrameTarget();
        }
    }

    public void CastBarUpdate(float percentage)
    {
        castingBarSlider.value = percentage;
    }

    public void CastBarDisplay(HealingSpell healingSpell)
    {
        castingBarPanel?.SetActive(healingSpell != null);
        if(healingSpell != null)
        {
            castingBarText.SetText(healingSpell.spellName);
        } else
        {
            castingBarText.SetText("");
        }
    }

    public void ClearUnitFrameTarget()
    {
        foreach (GameObject unitFrame in unitFrames)
        {
            unitFrame.GetComponent<UnitFrameScript>().border.SetActive(false);
        }
    }
}
