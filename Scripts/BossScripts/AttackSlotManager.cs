using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Aus https://www.trickyfast.com/2017/10/09/building-an-attack-slot-system-in-unity/
 * 
 * System, um Units um einen Boss herum einen Slot zuzuweisen, um Clumping zu verhindern
 */
public class AttackSlotManager : MonoBehaviour
{
    private List<GameObject> meleeSlots;
    public int count = 16;
    public float distance = 10f;

    private void Start()
    {
        meleeSlots = new List<GameObject>();
        for(int x = 0; x < count; x++)
        {
            meleeSlots.Add(null);
        }
    }


    public Vector3 GetSlotPosition(int index)
    {
        float degreesPerIndex = 360f / count;
        var pos = transform.position;
        var offset = new Vector3(0f, 0f, distance);
        return pos + (Quaternion.Euler(new Vector3(0f, degreesPerIndex * index, 0f)) * offset);
    }

    public int Reserve(GameObject attacker)
    {
        var bestPosition = transform.position;
        var offset = (attacker.transform.position - bestPosition).normalized * distance;
        bestPosition += offset;
        int bestSlot = -1;
        float bestDist = 99999f;
        for (int index = 0; index < meleeSlots.Count; ++index)
        {
            if (meleeSlots[index] != null)
                continue;
            var dist = (GetSlotPosition(index) - bestPosition).sqrMagnitude;
            if (dist < bestDist)
            {
                bestSlot = index;
                bestDist = dist;
            }
        }
        if (bestSlot != -1)
            meleeSlots[bestSlot] = attacker;
        return bestSlot;
    }

    void OnDrawGizmosSelected()
    {
        for (int index = 0; index < count; ++index)
        {
            if (meleeSlots == null || meleeSlots.Count <= index || meleeSlots[index] == null)
                Gizmos.color = Color.black;
            else
                Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetSlotPosition(index), 0.5f);
        }
    }
}
