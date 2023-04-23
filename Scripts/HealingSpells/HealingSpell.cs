using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "/Scripts/HealingSpells/SpellObjects", menuName ="HealingSpell")]
public class HealingSpell : ScriptableObject
{
    public float castingDuration;
    public float healingAmount;
    public string spellName;
}
