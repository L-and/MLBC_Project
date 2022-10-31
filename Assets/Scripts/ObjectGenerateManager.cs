using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerateManager : MonoBehaviour
{
    private Transform playerTransform;

    // 유닛의 스폰을 위한 변수
    [SerializeField]
    private float unitSpawnRangeFar; // 스폰가능 최대거리
    [SerializeField]
    private float unitSpawnRangeNear; //스폰가능 최소거리

    [SerializeField]
    private GameObject[] roads; // 도로들의 z값
    private float[] spawnAbleZPos;

    private Vector3[] unitPos;
    
    private bool spawning = true; // 코루틴을 사용해 일정시간마다 스폰하도록 함

    
    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        spawnAbleZPos = new float[roads.Length];
        for(int i = 0; i < roads.Length; i++)
        {
            spawnAbleZPos[i] = roads[i].transform.localPosition.z; // 오브젝트가 스폰 될 ZPos값을 초기화
        }

        
    }

    private void Update()
    {
        if(spawning)
            StartCoroutine(unitSpawnCoroutine());
    }

    IEnumerator unitSpawnCoroutine()
    {
        while(true)
        {
            spawning = false;
            yield return new WaitForSeconds(1.5f);
            Vector3 spawnPos = new Vector3(
                        spawnAbleZPos[(int)Random.Range(0, spawnAbleZPos.Length)], // 차선위치에 맞게
                        0,
                        playerTransform.position.z + Random.Range(unitSpawnRangeFar, unitSpawnRangeNear)// 플레이어z + 스폰범위중 랜덤
                        );

            Debug.Log("[UnitSpawnPosition]" + spawnPos.ToString());
            ObjectPool.GetObject(spawnPos);

            spawning = true;
        }

        
        
    }
}
