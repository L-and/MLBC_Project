using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어, 유닛 오브젝트앞에 다른 유닛이 있을때 속도를 변경하는 스크립트

public class UnitChecker : MonoBehaviour
{
    private UnitMove unitMove; // 부모 오브젝트의 UnitMove 컴포넌트

    void Start()
    {
        unitMove = transform.root.gameObject.GetComponent<UnitMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit" || other.tag == "Player")
            unitMove.changeMaxSpeed(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit" || other.tag == "Player")
            unitMove.changeMaxSpeed(null);
    }
}
