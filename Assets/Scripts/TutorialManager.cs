using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI descriptionText; // 튜툐리얼 설명 텍스트 필드

    [SerializeField]
    GameObject tutorialUIObject;

    [SerializeField]
    GameManager gameManager;

    private void Start()
    {   
        // 튜토리얼 시작 시 방향키 가이드 출력
        showControllGuide();
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            Time.timeScale = 1.0f;
            hideTutorialUI();
        }
    }

    // 방향키 튜툐리얼 UI
    public void showControllGuide()
    {
        descriptionText.text = "좌, 우 방향키로 좌우 이동";

        tutorialUIObject.SetActive(true);
    }

    public void showFeverGuide()
    {
        descriptionText.text = "좌우이동 시 다른 차와 스치면 피버게이지 증가";

        Time.timeScale = 0f;

        tutorialUIObject.SetActive(true);
    }

    public void hideTutorialUI()
    {
        // 플레이어 이동 컴포넌트 비활성화 시 활성화
        if(gameManager.isGameRunning == false)
            gameManager.isGameRunning = true;

        tutorialUIObject.SetActive(false);
    }
}
