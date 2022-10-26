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
    public GameObject[] road = new GameObject[2];
    private Vector3 roadOffset;

    // 슬라이드입력 관련 변수
    public bool isSlideActivate; // 슬라이드를 한번할때 차선을 1칸만 움직이기 위한 변수
    public float slideSensitivity; // 슬라이드 민감도

    // 키보드입력 관련 변수
    private bool isKeyInputEnabled;

    // 차선이동을 위한 변수
    private Vector3 targetPosition; // 이동할 위치
    private bool isLaneChanging; // 현재 차선이 변경중인지를 저장

    // 컴포넌트 변수들
    private UnitMove unitMove;
    private Rigidbody rigid;

    // Collider 오브젝트
    [SerializeField]
    private GameObject normalCollider; // 평상시 콜라이더
    [SerializeField]
    private GameObject laneChangingCollider; // 라인변경 시 콜라이더

    private void Start()
    {
        // 컴포넌트변수 초기화
        rigid = GetComponent<Rigidbody>();
        unitMove = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitMove>();

        // 일반변수 초기화
        roadOffset = road[1].transform.position - road[0].transform.position; // 도로와 도로사이의 간격을 지정해줌
        isSlideActivate = true; // 슬라이드 입력이 가능하게 초기설정
        targetPosition = Vector3.zero; // 차선변경시 목표위치를 설정
        isKeyInputEnabled = true;
        isLaneChanging = false;

        // 차량 콜라이더 활성, 비활성화 설정
        if (normalCollider == null || laneChangingCollider == null)
            Debug.Log("차량 콜라이더를 설정해주세요.");
        else
        {
            normalCollider.SetActive(true);
            laneChangingCollider.SetActive(false);
        }
    }


    private void Update()
    {
        playerControllTouch(); // 플레이어 조작[터치]
        playerControllKeyboard(); // 플레이어 조작[키보드]

        // 디버깅용
        //text.text = Time.time.ToString() + '\n' + unitMove.GetCurrentSpeed().speed.ToString();
    }

    private void colliderChange()
    {
        normalCollider.SetActive(!normalCollider.activeSelf);
        laneChangingCollider.SetActive(!laneChangingCollider.activeSelf);
    }
    
    // 차선변경을 시도하는 함수
    private void tryLaneChange()
    {
        if (isLaneChanging == false)
        {
            StartCoroutine(laneChangeCoroutine()); // 차선변경
        }
    }

    // 차선변경을 하는 코루틴
    IEnumerator laneChangeCoroutine()
    {
        Debug.Log("차선변경");
        isSlideActivate = false; isKeyInputEnabled = false; // 이동이 끝날떄까지 입력을 막아둠
        isLaneChanging = true;

        colliderChange(); // 차선변경 시 콜라이더로 변경
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * unitMove.GetCurrentSpeed().speed); // targetPosition으로 위치이동
            targetPosition.z = transform.position.z; // 앞으로 이동중인걸 반영하기위해 z값을 업데이트해줌

            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance <= 0.1f) // 타겟위치로 가까워졌으면
            {
                print("Stop");
                transform.position = targetPosition;

                break;
            }
            yield return null;

        }
        isLaneChanging = false; // 차선변경중 = false
        isSlideActivate = true; // 다시 터치입력을 받도록 해줌
        isKeyInputEnabled = true; // 다시 키입력을 받도록 해줌
        colliderChange();
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

                        changeTargetPosition(roadOffset);

                        
                    }
                    else if (screenTouch.deltaPosition.x < -slideSensitivity && roadCheck("left")) // 좌로 슬라이드
                    {
                        Debug.Log("[슬라이드]왼쪽");

                        changeTargetPosition(-roadOffset);

                    }
                    else if (screenTouch.deltaPosition.y < 0)
                    {
                        Debug.Log("[브레이크]");
                        unitMove.GetCurrentSpeed().speed -= 0.1f;
                    }
                }
            }


        }
    }

    //차선변경[키보드]
    private void playerControllKeyboard()
    {
        if (isKeyInputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && roadCheck("left")) // 왼쪽 방향키 입력
            {
                Debug.Log("[키보드]왼쪽");

                changeTargetPosition(-roadOffset);
                tryLaneChange();
                
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && roadCheck("right")) // 오른쪽 방향키 입력
            {
                Debug.Log("[키보드]오른쪽");

                changeTargetPosition(roadOffset);
                tryLaneChange();
                
            }
        }
    }

    private void changeTargetPosition(Vector3 changeVector)
    {
        targetPosition = transform.position + changeVector;
    }

    public void playerDie()
    {
        // 게임오버 처리
    }

}
