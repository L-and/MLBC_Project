using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// UI를 관리하는 UI매니저 스크립트
public class UIManager : MonoBehaviour
{
    private GameObject player;

    private int runningDistance; // 진행거리

    [SerializeField]
    private TextMeshProUGUI distanceUI;
    [SerializeField]
    private Slider feverSlider;

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
        distanceUI.text = Mathf.Round(ScoreManager.GetDistanceScore()).ToString();
    }

    private void UpdateFeverUI()
    {
        feverSlider.value = FeverManager.GetFeverValue();
    }
}
