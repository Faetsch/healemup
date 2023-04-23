using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFrameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> unitFrames;

    public void ClearUnitFrameTarget()
    {
        foreach (GameObject unitFrame in unitFrames)
        {
            unitFrame.GetComponent<UnitFrameScript>().border.SetActive(false);
        }
    }
}
