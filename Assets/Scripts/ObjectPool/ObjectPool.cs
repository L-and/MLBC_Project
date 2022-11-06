using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private void Awake()
    {
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

    public GameObject GetObject(Vector3 position) // 오브젝트를 사용
    {
        if (poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(this.transform);

            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(this.transform);

            return newObj;
        }
        
    }

    public void ReturnObject(GameObject obj) // 오브젝트를 반환
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        poolingObjectQueue.Enqueue(obj);
    }

}
