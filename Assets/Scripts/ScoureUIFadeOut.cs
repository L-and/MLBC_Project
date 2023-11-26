using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Scoure UI가 Fadeout후 오브젝트풀에 반환되게 만드는 클래스
public class ScoureUIFadeOut : MonoBehaviour
{
    TextMeshProUGUI text;
    Color originalColor;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
    }

    private void OnEnable()
    {
        StartCoroutine("FadeOut", 1.5f); // Fadeout효과 실행
    }


    // time초 후에 ScoreUI가 Fadeout되며 사라지도록 만드는 코루틴함수
    public IEnumerator FadeOut(float time)
    {
        Color color = text.color;
       
        while (color.a > 0f)
        {
            Debug.Log(color.a);
            color.a -= Time.deltaTime / time;
            text.color = color;
            yield return null;
        }
        // Fadeout종료 후 오브젝트 풀에 오브젝트 리턴
        text.color = originalColor; // 풀에 돌려주기 전 원래 색으로 바꿔줌
        gameObject.transform.parent.gameObject.GetComponent<ObjectPool>().ReturnObject(gameObject);
    }
}
