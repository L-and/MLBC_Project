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

    private void OnTriggerEnter(Collider other) // 다른유닛과 충돌할 때
    {
        Debug.Log("충돌시작");
        if (other.tag == "Unit")
            unitMove.ChangeSpeedWithOtherUnit(other.gameObject); // 다른유닛의 속도로 현재유닛의 속도를 조정
    }

    private void OnTriggerStay(Collider other) // 다른유닛과 충돌중일 때
    {
        Debug.Log("충돌중");
        if (other.tag == "Unit" && unitMove.GetCurrentSpeed().maxSpeed == unitMove.GetCurrentSpeed().speed) // 유닛이 최고속도에 도달해있으면 
            unitMove.ChangeSpeedWithOtherUnit(other.gameObject);
    }

    private void OnTriggerExit(Collider other) // 다른유닛과 충돌이 끝났을 때
    {
        Debug.Log("충돌끝");
        if (other.tag == "Unit")
            unitMove.ChangeSpeedWithOtherUnit(null); // 유닛의 속도를 원래대로 복원
    }
}
