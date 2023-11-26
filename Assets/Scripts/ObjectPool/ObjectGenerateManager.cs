using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectGenerateManager : MonoBehaviour
{
    private Transform playerTransform;

    // 오브젝트의 스폰을 위한 변수
    [SerializeField]
    float objectSpawnDelay;
    [SerializeField]
    [Tooltip("오브젝트들간 스폰거리(플레이어 \"앞뒤길이 + 알파\"로 설정)")]
    float objectSpawnOffset;
    [SerializeField]
    [Tooltip("오브젝트들이 생성되는 플레이어와 최소거리(화면밖에서 생성되게 해야함)")]
    private float objectSpawnRangeFar;
    [Tooltip("오브젝트들이 생성되는 플레이어와 최대거리")]
    private float objectSpawnRangeNear;

    [SerializeField]
    [Tooltip("메인화면에서 오브젝트들이 생성되는 플레이어와 최대거리")]
    private float objectSpawnRangeNearOnMain;
    [SerializeField]
    [Tooltip("게임화면에서 오브젝트들이 생성되는 플레이어와 최대거리")]
    private float objectSpawnRangeNearOnGame;

    [SerializeField]
    private float spawnYPos;

    [SerializeField] 
    private GameObject[] roads; // 도로들의 X좌표를 얻기위해 선언
    private float[] spawnAbleXPos;

    private Queue<Transform> objectTransformQueue; // 생성된 오브젝트들의 Transform을 담는 큐
    
    private bool spawning = true; // 코루틴을 사용해 일정시간마다 스폰하도록 함

    //
    ObjectPool objectPool;
    
    private void Start()
    {
        objectPool = GetComponent<ObjectPool>();

        objectTransformQueue = new Queue<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        spawnAbleXPos = new float[roads.Length];
        for(int i = 0; i < roads.Length; i++)
        {
            spawnAbleXPos[i] = roads[i].transform.position.x; // 오브젝트가 스폰 될 XPos값을 초기화
        }

        objectSpawnRangeNear = objectSpawnRangeNearOnMain;
    }

    private void Update()
    {
        if(spawning)
            StartCoroutine(ObjectSpawnCoroutine());

    }

    public void ChangeObjectSpawnRangeNear()
    {
        objectSpawnRangeNear = objectSpawnRangeNearOnGame;
    }

    IEnumerator ObjectSpawnCoroutine()
    {
        while(true)
        {
            spawning = false;
            yield return new WaitForSeconds(objectSpawnDelay);
            Vector3 spawnPos = new Vector3(
                        spawnAbleXPos[(int)Random.Range(0, spawnAbleXPos.Length)], // 차선위치에 맞게
                        spawnYPos,
                        playerTransform.position.z + Random.Range(objectSpawnRangeNear, objectSpawnRangeFar)// 플레이어z + 스폰범위중 랜덤
                        );

            if ((spawnPos = SpawnPositionResetting(spawnPos)) != Vector3.zero) // 스폰위치가 적절하도록 재조정
            {
                objectTransformQueue.Enqueue(objectPool.GetObject(spawnPos).GetComponent<Transform>()); // 오브젝트를 생성하고 트랜스폼을를 트랜스폼큐에 추가
            }
            spawning = true;
        } 
    }

    private Vector3 SpawnPositionResetting(Vector3 spawnPos) // 오브젝트사이로 플레이어가 지나갈수 있도록 스폰되게 SpawnPos를 재설정
    {
        if (objectTransformQueue.Count == 0) // 큐가 비어있으면 인자를 바로리턴
            return spawnPos;

        Vector3 tmpVectorA = new Vector3(0.0f, 0.0f, spawnPos.z);
        // 이미 스폰되어있는 오브젝트과 스폰할려는 위치가 충분한 거리를 두고있는지 검사(Z: SpawnOffset의 거리만큼 거리가 있는가?)
        for (int index = 0; index < objectTransformQueue.Count; index++)
        {
            Vector3 tmpVectorB = new Vector3(0.0f, 0.0f, objectTransformQueue.ToArray()[index].localPosition.z);

            float distance = Vector3.Distance(tmpVectorA, tmpVectorB); // spawnPos와 스폰되어있는 오브젝트들간의 거리를 얻은 후

            if (distance < objectSpawnOffset) // objectSpawnOffset보다 가까운곳에 스폰될려하면 스폰위치 조정
            {
                return FindEmptyPosition();
            }
        }

        return spawnPos;
    }

    private Vector3 FindEmptyPosition() // 현재 스폰된 오브젝트들 사이에서 스폰이 가능한 공간을 찾아 리턴 
    {
        // 오브젝트 트랜스폼 큐를 포지션별로 정렬하기위해 포지션 벡터배열로 변환
        Transform[] objectTransformArray = objectTransformQueue.ToArray();
        List<Vector3> objectPositionList = new List<Vector3>();
        Vector3[] objectPositionArray;

        for (int i = 0; i < objectTransformArray.Length; i++) // 플레이어와 SpawnRangeNear 만큼 떨어진 Transform부터 포지션에 저장
        {
            if (objectTransformArray[i].position.z - playerTransform.position.z > objectSpawnRangeNear)
            {
                objectPositionList.Add(objectTransformArray[i].position);
            }
        }

        // 벡터를 z값으로 오름차순으로 정렬

        objectPositionArray = objectPositionList.OrderBy(v => v.z).ToArray<Vector3>();

        for (int i=1; i< objectPositionArray.Length; i++)
        { 
            if ((objectPositionArray[i].z - objectPositionArray[i - 1].z) > objectSpawnOffset) // 오브젝트 사이간에 SpawnOffset만큼의 거리가 있는가?
                return new Vector3(roads[(int)Random.Range(0, roads.Length)].transform.position.x, objectPositionArray[i - 1].y, objectPositionArray[i - 1].z + objectSpawnOffset / 2); // 거리가 있다면 그 사이로 위치지정
        }

        int nearSpawnObjectCnt = 0; // 현재 오브젝트주변에 스폰해있는 오브젝트가 몇개인지?
        List<float> spawnedXPosition = new List<float>(); // 이미 스폰된 오브젝트들의 x값들
        for(int i=1; i<objectPositionArray.Length-1; i++)
        {
            spawnedXPosition.Add(objectPositionArray[i].x);
            if (objectPositionArray[i].z - objectPositionArray[i - 1].z < objectSpawnOffset) // 스폰간격보다 오브젝트들의 간격이 좁다면
            {
                spawnedXPosition.Add(objectPositionArray[i-1].x);
                nearSpawnObjectCnt++;
            }
            if (objectPositionArray[i+1].z - objectPositionArray[i].z < objectSpawnOffset)
            {
                spawnedXPosition.Add(objectPositionArray[i+1].x);
                nearSpawnObjectCnt++;
            }

            if (nearSpawnObjectCnt < 2) // 주변에 스폰가능한 도로가 있다면 = 주변에 스폰된 오브젝트가 2개 미만이라면
            {
                while(true)
                {
                    int index = (int)Random.Range(0, 3);
                    if (!spawnedXPosition.Exists(x => Mathf.Abs(x - roads[index].transform.position.x) < 0.01f)) // 다른 오브젝트가 없는 도로라면
                    {
                        return new Vector3(roads[index].transform.position.x, objectPositionArray[i].y, objectPositionArray[i].z);
                    }
                }
            }
        }
        return Vector3.zero;
        //return new Vector3(roads[(int)Random.Range(0,3)].transform.position.x, 0, objectPositionArray[objectPositionArray.Length - 1].z + objectSpawnOffset); // 없다면 제일 뒤의 오브젝트에 도로는 랜덤으로 SpawnOffset을 추가하여 위치지정
    }

    public void ObjectPosDequeue()
    {
        objectTransformQueue.Dequeue();
    }
}
