using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null; // 싱글톤패턴

    private int busStationCount; // 지나친 버스정류장의 수

    private float score; // 점수
    private float distanceScore; // 거리점수
    private float feverScore; // 피버점수
    public float stationScore; // 정류장 점수

    public float scoreMultiple; // 점수 배율

    public float intervalTime;

    // 필요한 컴포넌트들
    private UnitMove playerUnitMove;

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

    private void Start()
    {
        //컴포넌트 할당
        playerUnitMove = GameObject.Find("Player").GetComponent<UnitMove>();

        score = 0; // 점수 초기화
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        Instance.distanceScore = playerUnitMove.GetDistanceScore();
        Instance.score = Instance.distanceScore + Instance.feverScore;
    }

    public static float GetScore()
    {
        return Instance.score;
    }

    public static void AddFeverScore(float value)
    {
        Instance.feverScore += value;
    }

    public void SaveMaxScore() // 최고점수를 저장
    {
        if(Instance.score > PlayerPrefs.GetFloat("Score"))
        {
            PlayerPrefs.SetFloat("Score", Instance.score);
        }
    }

    public float GetMaxScore()
    {
        return PlayerPrefs.GetFloat("Score");
    }
}
