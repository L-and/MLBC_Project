using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 피버시스템을 관리하는 피버매니저 스크립트
public class FeverManager : MonoBehaviour
{
    public static FeverManager Instance = null; // 싱글톤

    private static float feverValue;

    private static bool isFever;

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

    private void Update()
    {
        StartCoroutine(UpdateIsFeverCoroutine());
    }

    public static float GetFeverValue()
    {
        return feverValue;
    }

    public static void AddFeverValue(float value)
    {
        feverValue += value;
    }

    IEnumerator UpdateIsFeverCoroutine()
    {
        while(true)
        {
            if(feverValue >= 100.0f)
            {
                isFever = true;
                Debug.Log("피버타임!");

                yield return null;
            }
        }
    }
}
