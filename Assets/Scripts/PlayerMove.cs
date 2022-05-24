using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    // 속도관련 변수
    public float accuacceleration; // 가속도
    [SerializeField] // 임시
    private float currentSpeed; // 현재 속도
    public float maxSpeed; // 최대속도

    public TextMeshProUGUI text;

    private void Update()
    {
        moveForward(); // 전진이동
        getInput(); 


    }

    // 플레이어 입력을 관리하는 함수
    private void getInput()
    {
        if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);

            if(screenTouch.phase == TouchPhase.Moved)
            {
                if (screenTouch.deltaPosition.x > 0)
                {
                    text.text = "우로 슬라이드";
                }
                else if(screenTouch.deltaPosition.x < 0)
                {
                    text.text = "좌로 슬라이드";
                }
            }
            else if(screenTouch.phase == TouchPhase.)
            {
                text.text = "터치";
            }
            
        }
    }

    // 버스를 앞으로 이동시키는 함수
    private void moveForward()
    {
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }


}
