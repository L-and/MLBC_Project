using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    // 속도관련 변수
    private float originalAccuacceleration; // 가속도
    private float originalSpeed; // 현재속도
    private float originalMaxSpeed; // 최대속도

    [SerializeField] public float accuacceleration; // 가속도
    [SerializeField] public float speed; // 현재속도
    [SerializeField] public float maxSpeed; // 최대속도

    // 컴포넌트 변수들
    private Rigidbody rigid;
    [SerializeField] private UnitChecker unitChecker;

    private void Start()
    {
        originalAccuacceleration = accuacceleration;
        originalSpeed = speed;
        originalMaxSpeed = maxSpeed;

        rigid = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveForward(); // 전진이동
        maxSpeedLock();
    }

    // 버스를 앞으로 이동시키는 함수
    private void moveForward()
    {
        speed += accuacceleration * Time.deltaTime;

        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    public void changeMaxSpeed(GameObject otherUnit) // 유닛앞에 다른 유닛이 있을 때 최대속도변경함수
    {
            if (otherUnit != null)
            {
                UnitMove otherUnitMove = otherUnit.GetComponent<UnitMove>();

            // 다른 유닛의 속도값으로 변경해줌
                if (speed > otherUnitMove.speed) // 이 유닛이 다른 유닛보다 속도가 빠를경우
                    speed = otherUnitMove.speed; // 다른 유닛의 속도로 변경

                accuacceleration = otherUnitMove.accuacceleration;
                maxSpeed = otherUnitMove.maxSpeed;
            }
            else if (otherUnit == null)
        {
            // 원래의 속도값들로 변경해줌
                accuacceleration = originalAccuacceleration;
                maxSpeed = originalMaxSpeed;
            }
    }

    private void maxSpeedLock()
    {
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

}
