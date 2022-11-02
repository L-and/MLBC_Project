using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerateManager : MonoBehaviour
{
    public static ObjectGenerateManager Instance = null;

    private Transform playerTransform;

    // 오브젝트의 스폰을 위한 변수

    
    [SerializeField]
    [Tooltip("오브젝트들간 스폰거리(플레이어 \"앞뒤길이 + 알파\"로 설정)")]
    float objectSpawnOffset;
    [SerializeField]
    [Tooltip("오브젝트들이 생성되는 플레이어와 최소거리(화면밖에서 생성되게 해야함)")]
    private float objectSpawnRangeFar;
    [SerializeField]
    [Tooltip("오브젝트들이 생성되는 플레이어와 최대거리")]
    private float objectSpawnRangeNear;

    [SerializeField] 
    private GameObject[] roads; // 도로들의 z값
    private float[] spawnAbleZPos;

    private Queue<Transform> objectTransformQueue; // 생성된 오브젝트들의 Transform을 담는 큐
    
    private bool spawning = true; // 코루틴을 사용해 일정시간마다 스폰하도록 함

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        objectTransformQueue = new Queue<Transform>();
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
            StartCoroutine(UnitSpawnCoroutine());
    }

    IEnumerator UnitSpawnCoroutine()
    {
        while(true)
        {
            spawning = false;
            yield return new WaitForSeconds(1.5f);
            Vector3 spawnPos = new Vector3(
                        spawnAbleZPos[(int)Random.Range(0, spawnAbleZPos.Length)], // 차선위치에 맞게
                        0,
                        playerTransform.position.z + Random.Range(objectSpawnRangeFar, objectSpawnRangeNear)// 플레이어z + 스폰범위중 랜덤
                        );

            spawnPos = SpawnPositionResetting(spawnPos); // 스폰위치가 적절하도록 재조정
            objectTransformQueue.Enqueue(ObjectPool.GetObject(spawnPos).GetComponent<Transform>()); // 오브젝트를 생성하고 트랜스폼을를 트랜스폼큐에 추가

            spawning = true;
        } 
    }

    private Vector3 SpawnPositionResetting(Vector3 spawnPos) // 오브젝트사이로 플레이어가 지나갈수 있도록 스폰되게 SpawnPos를 재설정
    {
        if (objectTransformQueue.Count == 0) // 큐가 비어있으면 인자를 바로리턴
            return spawnPos;

        Vector3 tmpVectorA = new Vector3(0.0f, 0.0f, spawnPos.z);
        // 이미 스폰되어있는 오브젝트과 스폰할려는 위치가 충분한 거리를 두고있는지 검사
        for (int index = 0; index < objectTransformQueue.Count - 1; index++)
        {
            Vector3 tmpVectorB = new Vector3(0.0f, 0.0f, objectTransformQueue.ToArray()[index].localPosition.z);

            float distance = Vector3.Distance(tmpVectorA, tmpVectorB); // spawnPos와 스폰되어있는 오브젝트들간의 거리를 얻은 후 

            Debug.Log(spawnPos.z);
            Debug.Log(objectTransformQueue.ToArray()[index].localPosition.z);

            if (distance < objectSpawnOffset) // unitSpawnOffset보다 가까운곳에 스폰될려하면 스폰위치 조정
                return spawnPos + new Vector3(0, 0, distance);
        }

        return spawnPos;
    }

    public static void UnitPosDequeue()
    {
        Instance.objectTransformQueue.Dequeue();
    }
}
