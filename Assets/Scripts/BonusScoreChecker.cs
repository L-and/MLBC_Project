using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScoreChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        
        if(tag == "Unit")
        {
            // 점수증가
        }
    }
}
