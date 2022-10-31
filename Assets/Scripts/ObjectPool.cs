using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // 싱글톤 적용

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    private void Initialize(int initCount) // 오브젝트 초기화
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private GameObject CreateNewObject() // 풀링할 오브젝트를 생성
    {
        var newObj = Instantiate(poolingObjectPrefab);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);

        return newObj;
    }

    public static GameObject GetObject(Vector3 position) // 오브젝트를 사용
    {
        Debug.Log("오브젝트 생성");
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.position = position;
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);

            return newObj;
        }
        
    }

    public static void ReturnObject(GameObject obj) // 오브젝트를 반환
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);

        Debug.Log("오브젝트 풀링 리턴");
    }

}
