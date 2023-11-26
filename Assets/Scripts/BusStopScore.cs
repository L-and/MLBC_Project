using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopScore : MonoBehaviour
{
    ScoreUIGenerator scoreUIGenerator;

    private void Start()
    {
        scoreUIGenerator = GameObject.Find("Floating UI Manager").GetComponent<ScoreUIGenerator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[OnTriggerEnter]정류장점수 획득!");
        if (other.tag == "Player")
        {
            int score = 500;
            ScoreManager.AddBusStopScore(score); // scoreManager에 점수추가

            scoreUIGenerator.DrawScoreUI(other.gameObject.transform.position, score); // 화면에 점수획득 UI 그리기

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
