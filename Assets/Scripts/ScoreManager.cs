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

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "BusStation")
            {
                print("정류장 경유");
                busStationCount++;

                float currentTime = Time.time;
                score = (stationScore * scoreMultiple) / (currentTime - intervalTime);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "BusStation")
            {
                intervalTime = Time.time;
            }
        }
    }
}
