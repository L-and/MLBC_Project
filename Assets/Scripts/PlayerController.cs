using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // 디버깅용 변수들
    public TextMeshProUGUI text;

    public GameObject[] roadChecker; // 차선변경 시 차선이 있는가 체크하는용도의 콜라이더

    // 도로간 offset을 얻기위한 변수
    public GameObject[] road;
    private Vector3 roadOffset;

    // 슬라이드입력 관련 변수
    public bool isSlideActivate; // 슬라이드를 한번할때 차선을 1칸만 움직이기 위한 변수
    public float slideSensitivity; // 슬라이드 민감도

    // 차선이동을 위한 변수
    private Vector3 targetPosition; // 이동할 위치

    // 컴포넌트 변수들
    private UnitMove unitMove;
    private Rigidbody rigid;

    private void Start()
    {
        // 컴포넌트변수 초기화
        rigid = GetComponent<Rigidbody>();
        unitMove = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitMove>();

        // 일반변수 초기화
        roadOffset = road[0].transform.position - road[1].transform.position; // 도로와 도로사이의 간격을 지정해줌
        isSlideActivate = true; // 슬라이드 입력이 가능하게 초기설정
        targetPosition = Vector3.zero; // 차선변경시 목표위치를 설정
    }


    private void Update()
    {
        playerControllTouch(); // 플레이어 조작[터치]
        playerControllKeyboard(); // 플레이어 조작[키보드]

        tryLaneChange(); // 차선변경이 입력되면 동작함

        // 디버깅용
        text.text = Time.time.ToString() + '\n' + unitMove.speed.ToString();
    }

    // 차선변경을 시도하는 함수
    private void tryLaneChange()
    {
        if (targetPosition != Vector3.zero) // 차선변경을 시도하면 targetPosition이 zero 가 아니게 됨
        {
            laneChange(); // 차선변경
        }
    }

    // 차선변경을 하는 함수
    private void laneChange()
    {
        if (Mathf.Abs(transform.position.x - targetPosition.x) >= 0.01) // targetPos와 0.01 이내로 가까워지기 전이라면
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * unitMove.speed); // targetPosition으로 위치이동
            targetPosition.z = transform.position.z; // 앞으로 이동중인걸 반영하기위해 z값을 업데이트해줌
        }
        else // 이동이 끝났으면 
        {
            targetPosition = Vector3.zero;
        }
    }

    // 이동하려는 방향에 차선이 있는지 체크하는 함수
    /// <summary>
    /// direction: "left" or "right"
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool roadCheck(string direction)
    {
        if (direction == "left") // 왼쪽차선이 있는지 체크
        {
            return roadChecker[0].GetComponent<RoadChecker>().isRoadExist;
        }
        else if (direction == "right") // 오른쪽차선에 있는지 체크
        {
            return roadChecker[1].GetComponent<RoadChecker>().isRoadExist;
        }

        return false;
    }

    // 차선변경[터치]
    private void playerControllTouch()
    {
        if (Input.touchCount == 1) // 터치가 입력됨
        {
            Touch screenTouch = Input.GetTouch(0); //터치의 정보를받아 screenTouch에 저장


            if (isSlideActivate)
            {
                if (screenTouch.phase == TouchPhase.Moved) // 슬라이드 했을때
                {
                    if (screenTouch.deltaPosition.x > slideSensitivity && roadCheck("right")) // 우로 슬라이드
                    {
                        Debug.Log("[슬라이드]오른쪽");

                        targetPosition = transform.position + transform.right;

                        isSlideActivate = false; // 더이상 터치입력이 되지않도록 설정
                    }
                    else if (screenTouch.deltaPosition.x < -slideSensitivity && roadCheck("left")) // 좌로 슬라이드
                    {
                        Debug.Log("[슬라이드]왼쪽");

                        targetPosition = transform.position - transform.right;

                        isSlideActivate = false; // 더이상 터치입력이 되지않도록 설정
                    }
                    else if (screenTouch.deltaPosition.y < 0)
                    {
                        Debug.Log("[브레이크]");
                        unitMove.speed -= 0.1f;
                    }
                }
            }


            if (screenTouch.phase == TouchPhase.Ended) // 터치가 끝나면
            {
                isSlideActivate = true; // 다시 터치입력을 받도록 해줌
            }
        }
    }

    //차선변경[키보드]
    private void playerControllKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && roadCheck("left")) // 왼쪽 방향키 입력
        {
            Debug.Log("[키보드]왼쪽");

            targetPosition = transform.position - transform.right;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && roadCheck("right")) // 오른쪽 방향키 입력
        {
            Debug.Log("[키보드]오른쪽");

            targetPosition = transform.position + transform.right;
        }
    }


}
