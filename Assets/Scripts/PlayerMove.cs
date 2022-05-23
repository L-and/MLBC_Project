using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 속도관련 변수
    public float accuacceleration; // 가속도
    [SerializeField] // 임시
    private float currentSpeed; // 현재 속도
    public float maxSpeed; // 최대속도


    private void Update()
    {
        moveForward(); // 전진이동

    }

    // 버스를 앞으로 이동시키는 함수
    private void moveForward()
    {
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }


}
