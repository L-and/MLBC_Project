using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 플레이어와 유닛이 스쳤는지 검사하는 스크립트
public class UnitCrossChecker : MonoBehaviour
{
    public UnityEvent tutorialEvent;

    private void Start()
    {
        tutorialEvent = new UnityEvent();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag == "Unit")
        {
            FeverManager.AddFeverValue();
            tutorialEvent.Invoke();
        }
    }
}
