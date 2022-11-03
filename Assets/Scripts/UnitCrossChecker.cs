using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어와 유닛이 스쳤는지 검사하는 스크립트
public class UnitCrossChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag == "Unit")
        {
            FeverManager.AddFeverValue(20.0f);
        }
    }
}
