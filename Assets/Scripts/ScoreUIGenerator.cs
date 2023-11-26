using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIGenerator : MonoBehaviour
{

    Camera camera;

    ObjectPool objectPool;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        objectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 점수획득이 발생한 위치에 점수UI를 띄워주는 함수
    public void DrawScoreUI(Vector3 position, int scoreValue)
    {
        Debug.Log("Score UI Draw!");
        // InGameUI의 자식오브젝트로 점수UI 생성
        GameObject scoreUI = objectPool.GetObject(camera.WorldToScreenPoint(position));
        scoreUI.GetComponent<TextMeshProUGUI>().SetText("+" + scoreValue.ToString());
    }
}
