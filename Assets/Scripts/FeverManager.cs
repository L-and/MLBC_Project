using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 피버시스템을 관리하는 피버매니저 스크립트
public class FeverManager : MonoBehaviour
{
    public static FeverManager Instance = null; // 싱글톤

    private UnitMove unitMove; // 피버타임을 키기위한 컴포넌트변수
    private PlayerController playerController; // 콜라이더를 키고 끄기위한 컴포넌트

    private float feverValue;
    public static bool isFever;

    [Tooltip("피버타임 지속시간")]
    [SerializeField]
    private float feverTimeSecond;

    [Tooltip("피버점수 증가값")]
    [SerializeField]
    private float incrementalValue; // 피버 증가값

    [Tooltip("피버콜라이더를 넣으시오")]
    [SerializeField]
    private GameObject feverCollider;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        unitMove = GameObject.Find("Player").GetComponent<UnitMove>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(UpdateIsFeverCoroutine());
    }

    public static float GetFeverValue()
    {
        return Instance.feverValue;
    }

    public static void AddFeverValue()
    {
        Instance.feverValue += Instance.incrementalValue;
    }

    IEnumerator UpdateIsFeverCoroutine()
    {
        while(true)
        {
            if (feverValue >= 100.0f)
            {
                isFever = true;

                feverCollider.SetActive(true);
                unitMove.OnFeverMode();
                Debug.Log("피버타임!");

                yield return new WaitForSeconds(feverTimeSecond);

                isFever = false;

                feverCollider.SetActive(false);
                unitMove.OffFeverMode();
                playerController.colliderReset();
                feverValue = 0.0f;

                isFever = false;
            }
            yield return null;
        }
    }




}
