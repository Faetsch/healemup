using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class PlayerCasting : MonoBehaviour
{

    [SerializeField] PlayerTargeting targeting;
    [SerializeField] UnityEvent<float> onCastUpdate;
    [SerializeField] UnityEvent<HealingSpell> onCastActive;
    //TODO - auslagern
    [SerializeField] public List<HealingSpell> spellBook = new List<HealingSpell>();


    private HealingSpell currentHealingSpell = null;

    //TODO - work in progress - In Basisklasse Heal auslagern und magic numbers extracten
    //TODO - Referenz auf coroutine merken, um Heal abbrechen zu können
    public IEnumerator CastHeal(HealingSpell healingSpell)
    {
        float castingTime = healingSpell.castingDuration;
        float castedTime = 0f;
        Transform target = targeting.target;
        UnitHealth unitHealthAtStart = target.GetComponent<UnitHealth>();

        while (castedTime < castingTime)
        {
            castedTime += Time.deltaTime;
            onCastUpdate?.Invoke(castedTime / castingTime);
            yield return null;
        }

        //In der Zeit kann das Ziel schon tot sein, auch wenn es zu Beginn des casts nicht tot war
        UnitHealth unitHealthAtEnd = target.GetComponent<UnitHealth>();
        if (!unitHealthAtEnd.isDead())
        {
            unitHealthAtEnd.Heal(healingSpell.healingAmount);
        }

        //castbar zurücksetzen
        onCastUpdate?.Invoke(0f);
        SetCastingSpell(null);
    }

    public void Cast(HealingSpell healingSpell)
    {
        if (currentHealingSpell == null && targeting.target != null)
        {
            SetCastingSpell(healingSpell);
            StartCoroutine(CastHeal(currentHealingSpell));
        }
    }

    private void SetCastingSpell(HealingSpell healingSpell)
    {
        this.currentHealingSpell = healingSpell;
        onCastActive?.Invoke(healingSpell);
    }

    //TODO - provisorisch zwei Healing Spells (scriptable objects) erzeugt und im InputManager auf 2 buttons gemappt. So nicht stehen lassen
    public void OnCastButton1(InputAction.CallbackContext context)
    {
        Cast(spellBook[0]);
    }

    public void OnCastButton2(InputAction.CallbackContext context)
    {
        Cast(spellBook[1]);
    }

}
