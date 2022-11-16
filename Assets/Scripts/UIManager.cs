using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// UI를 관리하는 UI매니저 스크립트
public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null; // 싱글톤패턴

    private GameObject player;

    private int runningDistance; // 진행거리

    [SerializeField]
    private TextMeshProUGUI distanceUI;
    [SerializeField]
    private Slider feverSlider;
    [SerializeField]
    private GameObject feverSliderFill; // 피버 슬라이더 아래의 FIll 오브젝트로 선택


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject MainUI;

    [SerializeField]
    private GameObject InGameUI;

    [SerializeField]
    private GameObject EndUI;

    IEnumerator blinkCoroutine = BlinkFeverSliderCoroutine();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceUI();
        UpdateFeverUI();
    }

    private void UpdateDistanceUI()
    {
        distanceUI.text = ((int)Mathf.Round(ScoreManager.GetScore())).ToString();
    }

    private void UpdateFeverUI()
    {
        feverSlider.value = FeverManager.GetFeverValue();
    }

    private static void SetScoreTextOnEndUI()
    {
        Instance.scoreText.text = ((int)ScoreManager.GetScore()).ToString();
    }

    public static void EnableMainUI()
    {   
        Instance.EndUI.SetActive(false); // 종료화면UI 비활성화
        Instance.MainUI.SetActive(true); // 인게임 UI 활성화

    }

    public static void EnableEndUI()
    {
        Instance.InGameUI.SetActive(false); // 인게임 UI 비활성화
        SetScoreTextOnEndUI(); // 플레이에 사용되는 오브젝트 비활성화

        Instance.EndUI.SetActive(true); // 종료화면UI 활성화
    }

    public void StartBlinkFeverUI()
    {
        StartCoroutine(blinkCoroutine);
        print("UI깜빡임 시작");
    }

    public void StopBlinkFeverUI()
    {
        StopCoroutine(blinkCoroutine);
    }

    static IEnumerator BlinkFeverSliderCoroutine()
    {
        while (true)
        {
            Instance.feverSliderFill.SetActive(false);
            yield return new WaitForSeconds(.3f);
            Instance.feverSliderFill.SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
    }
}
