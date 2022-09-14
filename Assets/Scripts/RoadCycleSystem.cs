using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Road1과 Road2가 순환하면서 플레이어의 앞쪽으로 Transform이 바뀌며 무한히 도로가 생성되는 것 처럼 보이는 시스템
public class RoadCycleSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] roads = new GameObject[2];

    private Transform playerTransform;

    public int cycleOffset;

    private float roadLength;

    private int roadIndex = 1;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        roadLength = roads[1].transform.position.z - roads[0].transform.position.z;
    }

    private void Update()
    {
        CycleRoad();
    }

    private void CycleRoad()
    {

        if(playerTransform.position.z - roads[roadIndex].transform.position.z > cycleOffset) // 플레이어의 뒤로 도로가 오면 앞으로 위치를 이동시켜줌
        {
            ChangeRoadIndex();
            roads[roadIndex].transform.position += new Vector3(0, 0, roadLength);
        }
    }

    private void ChangeRoadIndex()
    {
        if (roadIndex == 1)
            roadIndex = 0;
        else
            roadIndex = 1;
    }
}
