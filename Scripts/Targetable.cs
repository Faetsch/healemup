using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    private PlayerTargeting playerTargeting;
    public void Awake()
    {
        playerTargeting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTargeting>();
    }
    public void OnMouseDown()
    {
        playerTargeting.AcquireTarget(this.transform);
    }
}
