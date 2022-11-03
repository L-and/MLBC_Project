using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null; // 싱글톤패턴

    private int busStationCount; // 지나친 버스정류장의 수

    private float score; // 점수
    public float stationScore; // 정류장 점수

    public float scoreMultiple; // 점수 배율

    public float intervalTime;

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
        score = 0; // 점수 초기화
    }

    
}
