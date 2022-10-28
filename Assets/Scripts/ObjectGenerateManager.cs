using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerateManager : MonoBehaviour
{
    private Transform playerTransform;

    [SerializeField]
    private float spawnRangeFar;
    [SerializeField]
    private float spawnRangeNear;

    [SerializeField]
    private GameObject[] roads; // 도로들의 z값을 얻기위함
    private float[] spawnAbleZPos;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        spawnAbleZPos = new float[roads.Length];
        for(int i = 0; i < roads.Length; i++)
        {
            spawnAbleZPos[i] = roads[i].transform.position.z;
        }

        Debug.Log(spawnAbleZPos);
    }

    private void Update()
    {
        unitSpawn();
    }

    private void unitSpawn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPos = new Vector3(
                spawnAbleZPos[(int)Random.Range(0, spawnAbleZPos.Length)], // 차선위치에 맞게
                0,
                playerTransform.position.z + Random.Range(spawnRangeFar, spawnRangeNear)// 플레이어z + 스폰범위중 랜덤
                );

            Debug.Log("[UnitSpawnPosition]" + spawnPos.ToString());
            ObjectPool.GetObject(spawnPos);
        }
    }
}
