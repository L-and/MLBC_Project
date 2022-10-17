using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 플레이어의 진행거리를 표시하는 UI매니저 스크립트
public class DistanceUIManager : MonoBehaviour
{
    private GameObject player;

    private int runningDistance; // 진행거리

    public TextMeshProUGUI distanceUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        updateDistance();
        setDistanceUI();
    }

    private void updateDistance()
    {
        runningDistance = Mathf.RoundToInt(player.transform.position.z); // z 포지션을 int 형으로 전환해서 저장
    }

    private void setDistanceUI()
    {
        distanceUI.text = runningDistance.ToString();
    }
}
