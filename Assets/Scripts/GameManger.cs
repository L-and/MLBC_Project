using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] // 임시
    private int busStationCount; // 지나친 버스정류장의 수

    [SerializeField] // 임시
    private float score; // 승객만족도(점수)
    public float stationScore; // 정류장 점수

    public float scoreMultiple; // 점수 배율

    public float intervalTime;

    private void Start()
    {
        score = 0; // 점수 초기화
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "EndPoint")
            {
                print("스테이지 종료");
            }

            if (other.gameObject.tag == "BusStation")
            {
                print("정류장 경유");
                busStationCount++;

                float currentTime = Time.time;
                score = (stationScore * scoreMultiple) / (currentTime - intervalTime);
                print(currentTime - intervalTime);
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
