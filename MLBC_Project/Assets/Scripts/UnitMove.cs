using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [System.Serializable]
    public class Speed
    {
        public float speed; // 현재속도
        public float maxSpeed;// 최대속도
        public float accuacceleration; // 가속도
        public float breakPower; // 브레이크 파워

        public Speed(float speed, float maxSpeed, float accuacceleration, float breakPower)
        {
            this.speed = speed; 
            this.maxSpeed = maxSpeed;
            this.accuacceleration = accuacceleration;
            this.breakPower = breakPower;
        }
    }

    [SerializeField]private Speed originalSpeed;
    [SerializeField]private Speed currentSpeed;

    // 컴포넌트 변수들
    [SerializeField] private UnitChecker unitChecker;

    private void Start()
    {
        originalSpeed.speed = currentSpeed.speed;
        originalSpeed.accuacceleration = currentSpeed.accuacceleration;
        originalSpeed.maxSpeed = currentSpeed.maxSpeed;
        originalSpeed.breakPower = currentSpeed.breakPower;
    }

    void Update()
    {
        moveForward(); // 전진이동
        speedLock();
    }

    // 버스를 앞으로 이동시키는 함수
    private void moveForward()
    {
        currentSpeed.speed = currentSpeed.speed + currentSpeed.accuacceleration * Time.deltaTime;

        transform.position += Vector3.forward * currentSpeed.speed * Time.deltaTime;
    }

    //public void ChangeSpeedWithOtherUnit(GameObject otherUnit) // 유닛앞에 다른 유닛이 있을 때 최대속도변경함수
    //{
    //        if (otherUnit != null)
    //        {
    //            UnitMove otherUnitMove = otherUnit.GetComponent<UnitMove>();

    //            // 다른 유닛의 속도값으로 변경해줌
    //            if (currentSpeed.speed > otherUnitMove.currentSpeed.speed) // 이 유닛이 다른 유닛보다 속도가 빠를경우
    //            currentSpeed.speed = otherUnitMove.currentSpeed.speed; // 다른 유닛의 속도로 변경

    //            currentSpeed.accuacceleration = otherUnitMove.currentSpeed.accuacceleration;
    //            currentSpeed.maxSpeed = otherUnitMove.currentSpeed.maxSpeed;
    //        }
    //        else if (otherUnit == null)
    //        {
    //            // 원래의 속도값들로 변경해줌
    //            currentSpeed.accuacceleration = originalSpeed.accuacceleration;
    //            currentSpeed.maxSpeed = originalSpeed.maxSpeed;
    //        }
    //}

    private void speedLock()
    {
        if(currentSpeed.speed > currentSpeed.maxSpeed) // MaxSpeed
        {
            currentSpeed.speed = currentSpeed.maxSpeed;
        }

        if(currentSpeed.speed <= 10) // 스피드가 0 이하로 안떨어지게
        {
            currentSpeed.speed = 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        
        if(collisionObject.tag == "Unit") // 다른유닛과 충돌하면 게임오버
        {
            // 게임오버
        }
    }

    public void OnFeverMode()
    {
        currentSpeed.maxSpeed *= 2.0f;
        currentSpeed.speed *= 2.0f;
        currentSpeed.accuacceleration *= 2.0f;
    }

    public void OffFeverMode()
    {
        currentSpeed.maxSpeed /= 2.0f;
        currentSpeed.speed /= 2.0f;
        currentSpeed.accuacceleration /= 2.0f;
    }

    public Speed GetOriginalSpeed()
    {
        return originalSpeed;
    }

    public Speed GetCurrentSpeed()
    {
        return currentSpeed;
    }
    
    public float GetDistanceScore() // 플레이어의 진행거리를 반환
    {
        float distanceScore = 0;
        distanceScore = gameObject.transform.position.z; // 플레이어의 위치는 0부터 시작하므로 z포지션이 진행거리

        return distanceScore;
    }
}
