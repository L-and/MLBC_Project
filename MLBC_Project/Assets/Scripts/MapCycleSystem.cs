using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 맵들이 순환하며 반복하게 하는 스크립트
public class MapCycleSystem : MonoBehaviour
{
    public static MapCycleSystem Instance = null; // 싱글톤

    [SerializeField] private GameObject[] maps; // 맵 오브젝트들을 저장

    private Vector3[] initMapPositions;

    private Transform playerTransform;

    [Tooltip("플레이어 뒤로 cycleOffset만큼 오면 뒤의 도로를 앞으로 보내줌")]
    public int cycleOffset;

    private float[] mapLengths; // 맵들의 길이를 저장

    [Tooltip("다음맵의 인덱스로 지정")]
    private int cycleIndex = 1;

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
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        mapLengths = new float[maps.Length];
        for(int i=0; i<maps.Length; i++)
        {
            mapLengths[i] = maps[i].transform.Find("EndPosition").GetComponent<Transform>().localPosition.z; // 맵들의 길이(z값)를 저장
        }

        initMapPositions = new Vector3[maps.Length]; // 맵들의 초기위치를 저장
        
        for(int i=0; i<initMapPositions.Length; i++)
        {
            initMapPositions[i] = maps[i].transform.position;
        }

    }

    private void FixedUpdate()
    {
        CycleRoad();
    }

    private void CycleRoad() // 이전 맵을 현재 맵의 앞으로 이동시켜줌
    {
        int lastIndex = cycleIndex % maps.Length;
        int currentIndex = (cycleIndex + 1) % maps.Length;
        if (playerTransform.position.z - cycleOffset > maps[lastIndex].transform.position.z)
        {
            // 이전맵의 위치를 현재맵의 위치 + 현재맵의 z길이 로 변경
            maps[currentIndex].transform.position = maps[lastIndex].transform.position + new Vector3(0, 0, mapLengths[lastIndex]);
            cycleIndex++;

        }
    }

    public static void ResetMapPosition() // 맵들의 위치를 초기위치로 초기화
    {
        for(int i=0; i<Instance.initMapPositions.Length; i++)
        {
            Instance.maps[i].transform.position = Instance.initMapPositions[i];
        }
    }

}